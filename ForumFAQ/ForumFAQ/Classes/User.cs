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

        public string Name { get { return _name; } }



        private static HashSet<string> _allUsers = new();
        public User(string name) 
        {
            if (name is null || name.Length == 0 || name == null)
                throw new ArgumentNullException("Name cannot be null");
            if (_allUsers.Contains(name))
                throw new ArgumentException("Name Taken");
            _name = name;
            _allUsers.Add(name);
        }
    }
}
