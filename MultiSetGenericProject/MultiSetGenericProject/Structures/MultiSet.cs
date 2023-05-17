using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using km.Collections.MultiZbior;

namespace MultiSetGeneric.Structures
{
    public class MultiSet<T> : IMultiSet<T> where T : notnull
    {

        #region <<< Basics >>>

        private Dictionary<T, int> _multiSet = new Dictionary<T, int>();

        public static MultiSet<T> Empty { get { return new MultiSet<T>(); } }

        #endregion


        #region <<< Constructors >>>

        public MultiSet() {}

        public MultiSet(IEnumerable<T> sequence)
        {
            this.UnionWith(sequence);
        }

        public MultiSet(IEqualityComparer<T> comparer)
        {
            _multiSet = new Dictionary<T, int>(comparer);
        }

        public MultiSet(IEnumerable<T> sequence, IEqualityComparer<T> comparer)
        {
            _multiSet = new Dictionary<T, int>(comparer);
            foreach(T item in sequence)
            {
                this.Add(item);
            }
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
                return counter;
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


        #region <<< IMultiSet / ExpandedCollection >>>

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
            ThrowExceptionIfIsNull(other);

            foreach (T item in other)
                this.Add(item);
            return this;
        }
        public MultiSet<T> IntersectWith(IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            ThrowExceptionIfIsNull(other);

            MultiSet<T> intersectedSet = new MultiSet<T>();
            foreach (T item in other)
            {
                if (this.Contains(item))
                {
                    intersectedSet.Add(item);
                    this.Remove(item, 1);
                }
            }

            this.Clear();
            _multiSet = intersectedSet._multiSet;
            return this;
        }
        public MultiSet<T> ExceptWith(IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            ThrowExceptionIfIsNull(other);

            MultiSet<T> exceptedSet = new MultiSet<T>();
            foreach (T item in other)
            {
                if (this.Contains(item))
                    this.Remove(item, 1);
                else
                    exceptedSet.Add(item);
            }
            return this;
        }
        public MultiSet<T> SymmetricExceptWith(IEnumerable<T> other)
        {
            ThrowExceptionIfIsReadOnly();
            ThrowExceptionIfIsNull(other);

            foreach (T item in other)
            {
                if (this.Contains(item))
                    this.Remove(item, 1);
                else
                    this.Add(item, 1);
            }
            return this;
        }
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            ThrowExceptionIfIsNull(other);

            MultiSet<T> ms = new MultiSet<T>(other);
            foreach(T item in this)
            {
                if (ms.Contains(item))
                    ms.Remove(item, 1);
                else
                    return false;
            }
            return true;
        }
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if ( (this.IsSubsetOf(new MultiSet<T>(other))) && (this.Count != new MultiSet<T>(other).Count) )
                return true;
            return false;
        }
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return (new MultiSet<T>(other).IsSubsetOf(this));
        }
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return ( (this.IsSupersetOf(other)) && (this.Count != new MultiSet<T>(other).Count) );
        }
        public bool Overlaps(IEnumerable<T> other)
        {
            ThrowExceptionIfIsNull(other);

            foreach(T item in other)
            {
                if(this.Contains(item))
                    return true;
            }
            return false;
        }
        public bool MultiSetEquals(IEnumerable<T> other)
        {
            ThrowExceptionIfIsNull(other);

            MultiSet<T> msOther = new MultiSet<T>(other);
            if(this.Count != msOther.Count) 
                return false;
            foreach(T item in this)
            {
                if (msOther.Contains(item))
                    msOther.Remove(item, 1);
            }
            if (msOther.Count != 0)
                return false;
            return true;
        }
        public bool IsEmpty { get { return this.Count == 0 ? true : false; } }

        public IEqualityComparer<T> Comparer => _multiSet.Comparer;

        #endregion


        #region <<< More IMultiSet >>>

        public int this[T item] { get { return _multiSet[item]; } }

        public IReadOnlyDictionary<T, int> AsDictionary()
        {
            return GetCopyOfThis()._multiSet;
        }

        public IReadOnlySet<T> AsSet()
        {
            HashSet<T> set = new HashSet<T>();

            foreach(T item in this)
            {
                set.Add(item);
            }
            return set;
        }

        #endregion


        #region <<< Operators >>>

        public static MultiSet<T> operator +(MultiSet<T> first, MultiSet<T> second)
        {
            ThrowExceptionIfIsNull(first);
            ThrowExceptionIfIsNull(second);
            
            return GetCopyOfThat(first).UnionWith(GetCopyOfThat(second));
        }
        public static MultiSet<T> operator -(MultiSet<T> first, MultiSet<T> second)
        {
            ThrowExceptionIfIsNull(first);
            ThrowExceptionIfIsNull(second);

            return GetCopyOfThat(first).ExceptWith(GetCopyOfThat(second));
        }
        public static MultiSet<T> operator *(MultiSet<T> first, MultiSet<T> second)
        {
            ThrowExceptionIfIsNull(first);
            ThrowExceptionIfIsNull(second);

            return GetCopyOfThat(first).IntersectWith(GetCopyOfThat(second));
        }

        #endregion


        #region <<< Tools >>>

        private static void ThrowExceptionIfIsNull(IEnumerable<T> other)
        {
            if (other is null)
                throw new ArgumentNullException();
        }
        private T[] GetArrayOfThis()
        {
            T[] values = new T[this.Count];
            this.CopyTo(values, 0);
            return values;
        }
        private MultiSet<T> GetCopyOfThis()
        {
            MultiSet<T> copy = new MultiSet<T>();
            copy._multiSet = new Dictionary<T, int>(this._multiSet);
            return copy;
        }
        private static MultiSet<T> GetCopyOfThat(MultiSet<T> other)
        {
            MultiSet<T> copy = new MultiSet<T>();
            copy._multiSet = new Dictionary<T, int>(other._multiSet);
            return copy;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            foreach (var (k, v) in _multiSet)
            {
                for(int i = 0; i < v; i++)
                {
                    sb.Append(k + " ");
                }
            }
            return sb.ToString().Trim();
        }
        public string ToStringShort()
        {
            StringBuilder sb = new StringBuilder("");
            foreach(var k in _multiSet.Keys)
            {
                sb.AppendLine(k.ToString() + " " + _multiSet[k]);
            }
            return sb.ToString().Trim();
        }

        #endregion

    }
}
