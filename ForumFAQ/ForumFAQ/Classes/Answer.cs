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


        public string Id { get { return _id; } }
        public string Message { get { return _message; } }



        private static int s_idCounter = 0;
        public Answer(string text)
        {
            _id = $"A{s_idCounter}";
            s_idCounter++;

            if (text is null || text.Length == 0 || text == null)
                _message = "";
            else _message = text;
        }
        public Answer() : this("") { }
    }
}
