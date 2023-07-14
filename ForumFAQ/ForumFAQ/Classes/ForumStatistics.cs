using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumFAQ.Classes
{
    public class ForumStatistics
    {
        private Forum _forum;
        public ForumStatistics(Forum forum) 
        {
            OnNewAnswerAdded = OnNewAnswerAddedDefault;
            OnNewQuestionAdded = OnNewQuestionAddedDefault;
            _forum = forum;
        }

        #region <<< Store & Output >>>

        private int _totalQuestionCount; //   /All threads /Threads with answers
        private int _totalAnswerCount;
        private int _averageAnswerCount;
        private int _emptyThreads;

        public int TotalQuestionCount { get { return _totalQuestionCount; } }
        public int TotalAnswerCount { get { return _totalAnswerCount; } }
        public int AverageAnswerCount { get { return _averageAnswerCount; } }
        public int EmptyThreads { get { return _emptyThreads; } }
        public int ActiveThreads { get { return _totalQuestionCount - _emptyThreads; } }

        #endregion



        #region <<< Calculating >>>

        private void Recalculate() 
        {
            _totalQuestionCount = 0;
            _totalAnswerCount = 0;
            _averageAnswerCount = 0;
            _emptyThreads = 0;

            SortedDictionary<string, Question> allQuestions = _forum.DownloadAllQuestions;
            _totalQuestionCount = _forum.NumberOfQuestions;
            foreach(var question in allQuestions)
            {
                if (question.Value.NumberOfAnswers == 0)
                    _emptyThreads++;
                _totalAnswerCount += question.Value.NumberOfAnswers;
            }
            _averageAnswerCount = (int)Math.Round((double)_totalAnswerCount / _totalQuestionCount, 0);
        }

        #endregion



        #region <<< Notification >>>

        public EventHandler OnNewAnswerAdded;
        public void OnNewAnswerAddedDefault(object sender, EventArgs args)
        {
            Recalculate();
        }

        public EventHandler OnNewQuestionAdded;
        public void OnNewQuestionAddedDefault(object sender, EventArgs args)
        {
            Recalculate();
        }

        #endregion

    }
}
