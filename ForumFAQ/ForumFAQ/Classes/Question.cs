using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ForumFAQ.Classes;

namespace ForumFAQ.Classes
{
    public class Question// : IObservable<User>
    {
        public class AnswerEventArgs : EventArgs
        {
            public string NameOP { get; }
            public string QuestionId { get; }
            public string AnsweringUser { get; }
            public string AnswerId { get; }
            public string AnswerText { get; }
            public AnswerEventArgs(string nameOP,string questionId, string answeringUser, string answerId, string answerText)
            {
                NameOP = nameOP; QuestionId = questionId; AnsweringUser = answeringUser; AnswerId = answerId; AnswerText = answerText;
            }
        }
        public event EventHandler AnswerAdded = null;


        #region <<< Variables & Properties >>> 

        private string _id;
        private string _message;
        private string _username;
        private SortedDictionary<string, Answer> _answers = new();


        public string Id { get { return _id; } }
        public string Message { get { return _message; } }
        public string Username { get { return _username; } }

        public int NumberOfAnswers { get { return _answers.Count; } }


        public SortedDictionary<string, Answer> DownloadAllAnswers { get { return new SortedDictionary<string, Answer>(_answers); } }

        #endregion


        #region <<< Constructors >>>

        private static int s_idCounter = 0;
        public Question(string text, User user, Forum forum)
        {
            if(user is null)
                throw new ArgumentNullException("user");

            _id = $"Q{s_idCounter}";
            s_idCounter++;

            if (text is null || text.Length == 0 || text == null)
                _message = "";
            else _message = text;
            _username = user.Name;

            AnswerAdded += forum.OnAnswerAdded;
        }
        public Question(User user, Forum forum) : this("", user, forum) { }

        #endregion


        #region <<< Functions >>>

        public void AddAnswer(string answerText, User user)
        {
            Answer newAnswer = new Answer(answerText, user);
            _answers.Add(newAnswer.Id, newAnswer);
            user.AddAnswer(newAnswer);
            AnswerEventArgs notification = new AnswerEventArgs(Username ,Id, user.Name, newAnswer.Id, newAnswer.Message);
            AnswerAdded?.Invoke(this, notification);
        }

        #endregion
    }
}
