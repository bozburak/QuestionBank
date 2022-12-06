using Newtonsoft.Json;
using QuestionBankProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using static QuestionBankProject.Enumeration;

namespace QuestionBankProject
{
    internal class Program
    {
        internal static void Main(string[] args)
        {
            WriteToConsole("Question Bank");

            string answer = string.Empty;
            var jsonData = string.Empty;
            do
            {
                WriteToConsole("If you want to crud operations for question, write 'add', 'delete', 'find', 'create exam'");
                answer = Console.ReadLine();
                if (answer.ToLower() == "add")
                {
                    WriteToConsole("Write question type (Classic, Gap-Filling, MultipleChoice, TrueFalse)");
                    var questionType = Console.ReadLine();

                    if (questionType.ToLower() == QuestionType.GapFilling.ToString().ToLower())
                    {
                        WriteToConsole("Please order gaps and answers");
                        WriteToConsole("Write '-' for gaps when queston text");
                        WriteToConsole("Use ',' for answers when answer");
                        WriteToConsole("Use ',' for answers when correct answer");
                    }
                    if (questionType.ToLower() == QuestionType.MultipleChoice.ToString().ToLower())
                    {
                        WriteToConsole("Use ',' for answers when answer");
                    }

                    WriteToConsole("Write question text");
                    var text = Console.ReadLine();
                    WriteToConsole("Write question answer");
                    var questionAnswer = Console.ReadLine();
                    WriteToConsole("Write question correct answer");
                    var questionCorrectAnswer = Console.ReadLine();
                    WriteToConsole("Write question point");
                    var point = Console.ReadLine();
                    WriteToConsole("Write question difficulty (Low, Medium, High)");
                    var difficulty = Console.ReadLine();

                    var newQuestion = new Question(text, questionAnswer.Split(',').ToList(), questionCorrectAnswer.Split(',').ToList(), Convert.ToInt32(point), Enum.Parse<Difficulty>(difficulty, true).ToString(), Enum.Parse<QuestionType>(questionType, true).ToString());
                    AppendNewQuestion(newQuestion);
                }
                if (answer.ToLower() == "delete")
                {
                    WriteToConsole("Write question text");
                    var questionText = Console.ReadLine();
                    jsonData = System.IO.File.ReadAllText(FilePath());
                    var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();

                    foreach (var question in questionList.Where(x => x.QuestionText == questionText))
                    {
                        WriteToConsole($"Id: {question.Id} - Question: {question.QuestionText} - Point: {question.Point}");
                    }

                    WriteToConsole("If you want to delete a question, write id");
                    var questionId = Console.ReadLine();
                    var deleteQuestion = questionList.FirstOrDefault(x => x.Id == Convert.ToInt32(questionId));
                    if (deleteQuestion != null)
                    {
                        questionList.Remove(deleteQuestion);
                        jsonData = JsonConvert.SerializeObject(questionList);
                        System.IO.File.WriteAllText(FilePath(), jsonData);
                    }
                }
                if (answer.ToLower() == "find")
                {
                    jsonData = System.IO.File.ReadAllText(FilePath());
                    var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();
                    WriteToConsole("Write find choose (question text, question answer text, question correct answer, point, difficulty)");
                    var findChoice = Console.ReadLine();
                    if (findChoice.Trim().ToLower() == "questiontext")
                    {
                        WriteToConsole("Write Question Text");
                        var findText = Console.ReadLine();
                        foreach (var question in questionList.Where(x => x.QuestionText.Contains(findText)).OrderBy(x => x.Point))
                        {
                            WriteToConsole($"Id: {question.Id} - Question: {question.QuestionText} - Point: {question.Point}");
                        }
                    }
                    if (findChoice.Trim().ToLower() == "question answer text")
                    {
                        WriteToConsole("Write Question Answer Text");
                        var findText = Console.ReadLine();
                        foreach (var question in questionList.Where(x => x.Answers.Select(x => x).Contains(findText)).OrderBy(x => x.Point))
                        {
                            WriteToConsole($"Id: {question.Id} - Question Answer: {String.Join(", ", question.Answers)} - Point: {question.Point}");
                        }
                    }
                    if (findChoice.Trim().ToLower() == "questioncorrectanswer")
                    {
                        WriteToConsole("Write Question Correct Answer Text");
                        var findText = Console.ReadLine();
                        foreach (var question in questionList.Where(x => x.CorrectAnswers.Select(x => x).Contains(findText)).OrderBy(x => x.Point))
                        {
                            WriteToConsole($"Id: {question.Id} - Question Correct Answer: {String.Join(", ", question.CorrectAnswers)} - Point: {question.Point}");
                        }
                    }
                    if (findChoice.Trim().ToLower() == "point")
                    {
                        WriteToConsole("Write Question Point");
                        var findText = Console.ReadLine();
                        foreach (var question in questionList.Where(x => x.Point == Convert.ToInt32(findText)).OrderBy(x => x.Point))
                        {
                            WriteToConsole($"Id: {question.Id} - Question: {question.QuestionText} - Point: {question.Point}");
                        }
                    }
                    if (findChoice.Trim().ToLower() == "difficulty")
                    {
                        WriteToConsole("Write Question Difficulty (Low, Medium, High)");
                        var findText = Console.ReadLine();
                        foreach (var question in questionList.Where(x => x.Difficulty.ToLower() == Enum.Parse<Difficulty>(findText, true).ToString().ToLower()).OrderBy(x => x.Point))
                        {
                            WriteToConsole($"Id: {question.Id} - Question: {question.QuestionText}- Difficulty: {question.Difficulty} - Point: {question.Point}");
                        }
                    }
                }
                if (answer.ToLower() == "exit")
                {
                    jsonData = JsonConvert.SerializeObject(new List<Question>());
                    System.IO.File.WriteAllText(FilePath(), jsonData);
                    Environment.Exit(0);
                }
                if (answer.ToLower() == "createexam")
                {
                    WriteToConsole("Write Exam Type (test, classic, mix)");
                    var examType = Console.ReadLine();
                    var examQuestions = new List<Question>();

                    if (examType == "test")
                    {
                        jsonData = System.IO.File.ReadAllText(FilePath());
                        var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();
                        var point = 0;
                        foreach (var question in questionList.Where(x => x.QuestionType == QuestionType.MultipleChoice.ToString()))
                        {
                            if (point > 100)
                            {
                                break;
                            }
                            point += question.Point;
                            examQuestions.Add(question);
                        }
                    }
                    else if (examType == "classic")
                    {
                        jsonData = System.IO.File.ReadAllText(FilePath());
                        var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();
                        var point = 0;
                        foreach (var question in questionList.Where(x => x.QuestionType == QuestionType.Classic.ToString()))
                        {
                            if (point > 100)
                            {
                                break;
                            }
                            point += question.Point;
                            examQuestions.Add(question);
                        }
                    }
                    else if (examType == "mix")
                    {
                        jsonData = System.IO.File.ReadAllText(FilePath());
                        var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();
                        var point = 0;
                        foreach (var question in questionList.OrderBy(x => Guid.NewGuid()))
                        {
                            if (point > 100)
                            {
                                break;
                            }
                            point += question.Point;
                            examQuestions.Add(question);
                        }
                    }
                    var examPoint = 0;
                    foreach (var question in examQuestions)
                    {
                        WriteToConsole(question.QuestionText);
                        WriteToConsole("Answer:");
                        var questionAnswer = Console.ReadLine();
                        if (question.Answers.Select(x => x.Trim().ToLower()).Contains(questionAnswer.Trim().ToLower()))
                            examPoint += question.Point;
                    }

                    jsonData = JsonConvert.SerializeObject(examQuestions);
                    System.IO.File.WriteAllText(@"..\..\..\Data\exam.txt", jsonData);
                }
            } while (answer.ToLower() == "add" || answer.ToLower() == "find" || answer.ToLower() == "delete");

            jsonData = JsonConvert.SerializeObject(new List<Question>());
            System.IO.File.WriteAllText(FilePath(), jsonData);
            Environment.Exit(0);

            static void WriteToConsole(string message, bool insertNewlineBefore = true)
            {
                Console.WriteLine(message);
                if (insertNewlineBefore)
                    Console.Write(System.Environment.NewLine);
            }

            static void AppendNewQuestion(Question question)
            {
                var jsonData = System.IO.File.ReadAllText(FilePath());
                var questionList = JsonConvert.DeserializeObject<List<Question>>(jsonData) ?? new List<Question>();
                questionList.Add(question);
                jsonData = JsonConvert.SerializeObject(questionList);
                System.IO.File.WriteAllText(FilePath(), jsonData);
            }

            static string FilePath()
            {
                return @"..\..\..\Data\Questions.json";
            }
        }
    }
}