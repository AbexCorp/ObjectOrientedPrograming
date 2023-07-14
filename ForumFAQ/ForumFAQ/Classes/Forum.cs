using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ForumFAQ.Classes
{
    public class Forum
    {
        public class QuestionEventArgs : EventArgs
        {
            public string QuestionId { get; }
            public string QuestionUser { get; }
            public QuestionEventArgs(string questionId, string questionUser)
            {
                QuestionId = questionId; QuestionUser = questionUser;
            }
        }

        #region <<< Variables & Properties & Constructor >>>

        private SortedDictionary<string, Question> _questions = new();

        private SortedDictionary<string, User> _users = new();
        private HashSet<string> _forumFollowers = new();
        private HashSet<string> _selfFollowers = new();


        public int NumberOfQuestions { get {  return _questions.Count; } }
        public SortedDictionary<string, Question> DownloadAllQuestions { get { return new SortedDictionary<string, Question>(_questions); } }

        public ForumStatistics stats;

        public Forum() 
        {
            stats = new ForumStatistics(this);
            OnAnswerAdded = OnAnswerAddedDefault;
            NewAnswerForum += stats.OnNewAnswerAdded;
            NewQuestion += stats.OnNewQuestionAdded;
        }

        #endregion


        #region <<< Functions >>>

        public User CreateNewUser(string username)
        {
            if (username is null || username.Length == 0 || username == null)
                throw new ArgumentNullException("Name cannot be null");
            if (_users.ContainsKey(username))
                throw new ArgumentException("Name Taken");
            User newUser = new User(username);
            _users.Add(username, newUser);
            return newUser;
        }
        public Question AddQuestion(string questionText, User user)
        {
            Question newQuestion = new Question(questionText, user, this);
            _questions.Add(newQuestion.Id, newQuestion);
            user.AddQuestion(newQuestion);
            QuestionEventArgs notification = new QuestionEventArgs(newQuestion.Id, newQuestion.Username);
            NewQuestion?.Invoke(this, notification);
            return newQuestion;
        }
        public SortedDictionary<string, Question> DownloadAllUserQuestions(User user)
        {
            return user.DownloadAllQuestions;
        }
        public SortedDictionary<string, Answer> DownloadAllUserAnswers(User user)
        {
            return user.DownloadAllAnswers;
        }
        public void PrintWholeForum()
        {
            foreach(var question in _questions)
            {
                Console.WriteLine($"{question.Value.Id} {question.Value.Username}");
                Console.WriteLine($"{question.Value.Message}");
                Console.WriteLine();
                foreach(var answer in question.Value.DownloadAllAnswers)
                {
                    Console.WriteLine($"    {answer.Value.Id} {answer.Value.Username}");
                    Console.WriteLine($"    {answer.Value.Message}");
                    Console.WriteLine();
                }
                Console.WriteLine();
                Console.WriteLine();
            }
        }

        #endregion


        #region <<< Subscribing >>>

        public void SubscribeForum(User user) 
        {
            if (_forumFollowers.Contains(user.Name))
                return;
            else
            {
                _forumFollowers.Add(user.Name);
                NewAnswerForum += user.OnNewAnswerAdded;
                NewQuestion += user.OnNewQuestionAdded;
            }
        }
        public void UnsubscribeForum(User user)
        {
            if (_forumFollowers.Contains(user.Name))
                _forumFollowers.Remove(user.Name);
            else
            {
                NewAnswerForum -= user.OnNewAnswerAdded;
                NewQuestion -= user.OnNewQuestionAdded;
                return;
            }
        }
        public void SubscribeSelf(User user)
        {
            if (_selfFollowers.Contains(user.Name))
                return;
            else
                _selfFollowers.Add(user.Name);
        }
        public void UnsubscribeSelf(User user)
        {
            if (_selfFollowers.Contains(user.Name))
                _selfFollowers.Remove(user.Name);
            else
                return;
        }

        #endregion


        #region <<< Notifications >>>

        public event EventHandler NewAnswerForum = null;
        public event EventHandler NewAnswerOwner = null;

        public event EventHandler NewQuestion = null;


        public EventHandler OnAnswerAdded;

        public void OnAnswerAddedDefault(object sender, EventArgs args)
        {
            Question.AnswerEventArgs args1 = (Question.AnswerEventArgs)args;
            NewAnswerForum?.Invoke(this, args);
            bool forumSubscriber = _forumFollowers.Contains(args1.NameOP);
            bool selfSubscriber = _selfFollowers.Contains(args1.NameOP);
            if( !forumSubscriber && selfSubscriber )
            {
                NewAnswerOwner = _users[args1.NameOP].OnNewAnswerAdded;
                NewAnswerOwner?.Invoke(this, args);
                NewAnswerOwner = null;
            }
        }

        #endregion
    }
}
