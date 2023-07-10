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
        private string _id;
        private string _message;
        private SortedDictionary<string, Answer> _answers = new();


        public string Id { get { return _id; } }
        public string Message { get { return _message; } }



        private static int s_idCounter = 0;
        public Question(string text)
        {
            _id = $"Q{s_idCounter}";
            s_idCounter++;

            if (text is null || text.Length == 0 || text == null)
                _message = "";
            else _message = text;
        }
        public Question() : this("") { }


        public void AddAnswer(string answerText) //User user    also needed
        {
            //Answer newAnswer = new Answer(answerText);
            //_answers.Add(newAnswer.Id, newAnswer);
            Console.WriteLine( Calculator(1,2,3, Sum) );
            Console.WriteLine( Calculator(1,2,3, Sub) );
            Console.WriteLine( Calculator(1,2,3, Mul) );
            Console.WriteLine( Calculator(1,2,3, (x,y,z) => y*z-x ));
            throw new NotImplementedException();
        }







        public delegate int Calculate(int x, int y, int z);
        public int Calculator(int x, int y, int z, Calculate c)
        {
            return c(x, y, z);
        }

        public int Sum(int x, int y, int z) { return x + y + z; }
        public int Sub(int x, int y, int z) { return x- y - z; }
        public int Mul(int x, int y, int z) { return x*y*z; }
    }
}
