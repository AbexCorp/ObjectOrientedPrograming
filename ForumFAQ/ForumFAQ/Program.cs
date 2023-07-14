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
            forum.AddQuestion("How to rob a bank with cool background music?", user1);
            forum.UnsubscribeForum(user2);
            forum._questions["Q0"].AddAnswer("Don't", user2);
            forum._questions["Q0"].AddAnswer("Don't do", user2);
            forum._questions["Q0"].AddAnswer("Don't do that", user2);
        }
    }
}