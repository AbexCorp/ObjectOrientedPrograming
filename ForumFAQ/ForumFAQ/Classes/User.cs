using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ForumFAQ.Classes
{
    public class User
    {
        private readonly string _name;
        private SortedDictionary<string, Question> _questions = new();
        private SortedDictionary<string, Answer> _answers = new();

        public string Name { get { return _name; } }
        public SortedDictionary<string, Question> DownloadAllQuestions { get { return new SortedDictionary<string, Question>(_questions); } }
        public SortedDictionary<string, Answer> DownloadAllAnswers { get { return new SortedDictionary<string, Answer>(_answers); } }


        public User(string name) 
        {
            _name = name;
            OnNewAnswerAdded = OnNewAnswerAddedDefault;
            OnNewQuestionAdded = OnNewQuestionAddedDefault;
        }



        public void AddAnswer(Answer answer)
        {
            _answers.Add(answer.Id, answer);
        }
        public void AddQuestion(Question question)
        {
            _questions.Add(question.Id, question);
        }



        #region <<< Notifications >>>

        public EventHandler OnNewAnswerAdded;
        public void OnNewAnswerAddedDefault(object sender, EventArgs args)
        {
            Question.AnswerEventArgs args1 = (Question.AnswerEventArgs)args;
            if(!_questions.ContainsKey(args1.QuestionId))
                Console.WriteLine($"User {args1.AnsweringUser} answered {args1.AnswerId} to question {args1.QuestionId} by {args1.NameOP}");
            else
                Console.WriteLine($"User {args1.AnsweringUser} answered {args1.AnswerId} to your question {args1.QuestionId}");
        }

        public EventHandler OnNewQuestionAdded;
        public void OnNewQuestionAddedDefault(object sender, EventArgs args)
        {
            Forum.QuestionEventArgs args1 = (Forum.QuestionEventArgs)args;
            Console.WriteLine($"User {args1.QuestionUser} asked new question {args1.QuestionId}");
        }

        #endregion

    }
}
