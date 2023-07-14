using System;
using ForumFAQ.Classes;

namespace ForumFAQLib
{
    class Program
    {
        static void Main(string[] args)
        {
            Forum forum = new();
            User user1 = forum.CreateNewUser("user1");
            User user2 = forum.CreateNewUser("user2");
            User user3 = forum.CreateNewUser("user3");

            forum.SubscribeForum(user3);
            Question question1 = forum.AddQuestion("How to rob a bank with cool background music?", user1);
            forum.SubscribeSelf(user1);
            question1.AddAnswer("Don't", user2);
            forum.UnsubscribeSelf(user1);
            forum.UnsubscribeForum(user2);
            question1.AddAnswer("Don't do", user2);
            Question question2 = forum.AddQuestion("How to rob a bank with cool background music?", user2);
            question1.AddAnswer("Don't do that", user2);
            question2.AddAnswer("Please Please Please ", user1);
            question2.AddAnswer("Please do Please do Please do", user1);
            question2.AddAnswer("Please do that Please do that Please do that", user1);
            Question question3 = forum.AddQuestion("How to rob a bank with cool background music?", user1);

            Console.WriteLine();
            Console.WriteLine($"TotalQuestionCount: {forum.stats.TotalQuestionCount}");
            Console.WriteLine($"TotalAnswerCount: {forum.stats.TotalAnswerCount}");
            Console.WriteLine($"AverageAnswerCount: {forum.stats.AverageAnswerCount}");
            Console.WriteLine($"EmptyThreads: {forum.stats.EmptyThreads}");
            Console.WriteLine($"ActiveThreads: {forum.stats.ActiveThreads}");
            Console.WriteLine();
            Console.WriteLine();

            Console.WriteLine("All questions of user1");
            foreach(var x in forum.DownloadAllUserQuestions(user1))
            {
                Console.WriteLine(x.Value.Id);
            }
            Console.WriteLine("All answers of user1");
            foreach(var x in forum.DownloadAllUserAnswers(user1))
            {
                Console.WriteLine(x.Value.Id);
            }


            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
            forum.PrintWholeForum();
        }
    }
}