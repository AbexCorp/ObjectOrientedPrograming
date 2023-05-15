using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using km.Collections.MultiZbior;

namespace MultiSetGeneric.Structures
{
    public class MultiSet<T> : ICollection<T>, IEnumerable<T> /*: IMultiSet<T>*/ where T : notnull
    {

        private Dictionary<T, int> _multiSet = new Dictionary<T, int>();


        public static MultiSet<T> Empty { get { return new MultiSet<T>(); } }


        #region <<< Constructors >>>

        public MultiSet() {}

        public MultiSet(IEnumerable<T> sequence)
        {
            this.UnionWith(sequence);
        }


        #endregion


        #region <<< ICollection<T> >>>

        public int Count 
        {
            get 
            {
                int counter = 0;
                foreach (var item in _multiSet)
                {
                    counter = counter + item.Value;
                }
                return _multiSet.Count; 
            } 
        }

        public bool IsReadOnly => false;
        private void ThrowExceptionIfIsReadOnly()
        {
            if (IsReadOnly) throw new NotSupportedException("This multiSet is ReadOnly");
        }


        public void Add(T item)
        {
            ThrowExceptionIfIsReadOnly();
            
            if(_multiSet.ContainsKey(item))
                _multiSet[item]++;
            else
                _multiSet.Add(item, 1);
        }
        public bool Remove(T item)
        {
            ThrowExceptionIfIsReadOnly();

            try
            {
                _multiSet.Remove(item);
            }
            catch (Exception) { return false; }
            return true;
        }
        public bool Contains(T item)
        {
            if(_multiSet.ContainsKey(item))
                return true;
            return false;
        }
        public void Clear()
        {
            ThrowExceptionIfIsReadOnly();

            _multiSet.Clear();
        }
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array.Length - arrayIndex < this.Count)
                throw new ArgumentException("Array is to small to copy all elements");

            foreach (T item in _multiSet.Keys)
            {
                for(int i = 0; i < _multiSet[item]; i++)
                {
                    array[arrayIndex] = item;
                    arrayIndex++;
                }
            }
        }

        #endregion


        #region <<< IEnumerable<T> >>>

        public IEnumerator<T> GetEnumerator()
        {
            T[] values = GetArrayOfThis();
            for(int i = 0; i < Count; i++)
            {
                yield return values[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion


        #region <<< ExpandedCollection >>>

        public MultiSet<T> Add (T item, int numberOfItems = 1)
        {
            ThrowExceptionIfIsReadOnly();

            if (_multiSet.ContainsKey(item))
                _multiSet[item] += numberOfItems;
            else
                _multiSet.Add(item, numberOfItems);
            return this;
        }
        public MultiSet<T> Remove (T item,  int numberOfItems = 1)
        {
            ThrowExceptionIfIsReadOnly();

            if (!this.Contains(item))
                return this;
            if (numberOfItems >= _multiSet[item])
            {
                this.Remove(item);
                return this;
            }
            else
                _multiSet[item] -= numberOfItems;
            return this;
        }
        public MultiSet<T> RemoveAll(T item)
        {
            ThrowExceptionIfIsReadOnly();
            this.Remove(item);
            return this;
        }
        public MultiSet<T> UnionWith (IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            if(other is null)
                throw new ArgumentNullException();

            foreach (T item in other)
                this.Add(item);
            return this;
        }
        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            if (other is null)
                throw new ArgumentNullException();

            MultiSet<T> intersectedSet = new MultiSet<T>();
            foreach (T item in other)
            {
                if (this.Contains(item))
                {
                    intersectedSet.Add(item);
                    this.Remove(item);
                }
            }

            this.Clear();
            _multiSet = intersectedSet._multiSet;
            return this;
        }
        // modyfikuje bieżący multizbiór tak, aby zawierał tylko te 
        // które nie wystepują w `other`
        // zgłasza `ArgumentNullException` jeśli `other` jest `null`
        // zgłasza `NotSupportedException` jeśli multizbior jest tylko do odczytu
        // zwraca referencję tej instancji multizbioru (`this`)
        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            if (other is null)
                throw new ArgumentNullException();

            MultiSet<T> exceptedSet = new MultiSet<T>();
            foreach (T item in this)
            {
                //This needs to treat each element as different thing.
                //for example (1,1,2,3,3,3,4,5) and (1,2,2,3)
                //should give (1,3,3,4,5)    removed [1,2,3]
                //and not (4,5)              removed [1,1,2,3,3,3]

                //if(!other.Contains(item))
                    //exceptedSet.Add(item);
            }
            return this;
        }

        #endregion


        #region <<< Tools >>>

        private T[] GetArrayOfThis()
        {
            T[] values = new T[this.Count];
            this.CopyTo(values, 0);
            return values;
        }

        #endregion
    }
}
