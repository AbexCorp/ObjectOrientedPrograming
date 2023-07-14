using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumFAQ.Classes
{
    public class Answer
    {
        private string _id;
        private string _message;
        private string _username;


        public string Id { get { return _id; } }
        public string Message { get { return _message; } }
        public string Username { get { return _username; } }



        private static int s_idCounter = 0;
        public Answer(string text, User user)
        {
            if(user is null)
                throw new ArgumentNullException("user");

            _id = $"A{s_idCounter}";
            s_idCounter++;

            _username = user.Name;
            if (text is null || text.Length == 0 || text == null)
                _message = "";
            else 
                _message = text;
        }
        public Answer(User user) : this("", user) { }
    }
}
