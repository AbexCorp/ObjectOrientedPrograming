using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using km.Collections.MultiZbior;

namespace MultiSetGeneric.Structures
{
    public class MultiSet<T> /*: IMultiSet<T>*/ where T : notnull
    {

        private Dictionary<T, int> _multiSet = new Dictionary<T, int>();

        public MultiSet() {}


        #region <<< ICollection<T> >>>

        public int Count { get { return _multiSet.Count; } }

        public bool IsReadOnly { get { return false; } }


        public void Add (T item)
        {
            if (IsReadOnly) throw new NotSupportedException("This multiSet is readOnly");
            
            if(_multiSet.ContainsKey(item))
                _multiSet[item]++;
            else
                _multiSet.Add(item, 1);
        }

        #endregion
    }
}
