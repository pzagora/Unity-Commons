using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
// ReSharper disable All

namespace Commons
{
    public class ObservableList<T> :  
        BaseObservable,
        ICollection<T>,
        IEnumerable<T>,
        IEnumerable,
        IList<T>,
        IReadOnlyCollection<T>,
        IReadOnlyList<T>,
        ICollection,
        IList
    {
        private readonly List<T> innerList;

        public ObservableList() { innerList = new List<T>();}
        public ObservableList(IEnumerable<T> collection) { innerList = new List<T>(collection);}
        public ObservableList(int capacity) { innerList = new List<T>(capacity);}
        
        public void CopyTo(Array array, int index) => ((ICollection)innerList).CopyTo(array, index);
        public bool IsSynchronized => ((ICollection)innerList).IsSynchronized;
        public object SyncRoot => ((ICollection)innerList).SyncRoot;
        public bool Contains(object value) => ((IList)innerList).Contains(value);
        public int IndexOf(object value) => ((IList)innerList).IndexOf(value);
        public bool IsFixedSize => ((IList)innerList).IsFixedSize;
        bool IList.IsReadOnly => false;
        public ReadOnlyCollection<T> AsReadOnly() => innerList.AsReadOnly();
        public int BinarySearch(int index, int count, T item, IComparer<T> comparer) => innerList.BinarySearch(index, count, item, comparer);
        public int BinarySearch(T item) => innerList.BinarySearch(item);
        public int BinarySearch(T item, IComparer<T> comparer) => innerList.BinarySearch(item, comparer);
        public bool Contains(T item) => innerList.Contains(item);
        public List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter) => innerList.ConvertAll(converter);
        public void CopyTo(int index, T[] array, int arrayIndex, int count) => innerList.CopyTo(index, array, arrayIndex, count);
        public void CopyTo(T[] array) => innerList.CopyTo(array);
        public void CopyTo(T[] array, int arrayIndex) => innerList.CopyTo(array, arrayIndex);
        public bool Exists(Predicate<T> match) => innerList.Exists(match);
        public T Find(Predicate<T> match) => innerList.Find(match);
        public List<T> FindAll(Predicate<T> match) => innerList.FindAll(match);
        public int FindIndex(int startIndex, int count, Predicate<T> match) => innerList.FindIndex(startIndex, count, match);
        public int FindIndex(int startIndex, Predicate<T> match) => innerList.FindIndex(startIndex, match);
        public int FindIndex(Predicate<T> match) => innerList.FindIndex(match);
        public T FindLast(Predicate<T> match) => innerList.FindLast(match);
        public int FindLastIndex(int startIndex, int count, Predicate<T> match) => innerList.FindLastIndex(startIndex, count, match);
        public int FindLastIndex(int startIndex, Predicate<T> match) => innerList.FindLastIndex(startIndex, match);
        public int FindLastIndex(Predicate<T> match) => innerList.FindLastIndex(match);
        public void ForEach(Action<T> action) => innerList.ForEach(action);
        public List<T> GetRange(int index, int count) => innerList.GetRange(index, count);
        public int IndexOf(T item) => innerList.IndexOf(item);
        public int IndexOf(T item, int index) => innerList.IndexOf(item, index);
        public int IndexOf(T item, int index, int count) => innerList.IndexOf(item, index, count);
        public int LastIndexOf(T item) => innerList.LastIndexOf(item);
        public int LastIndexOf(T item, int index) => innerList.LastIndexOf(item, index);
        public int LastIndexOf(T item, int index, int count) => innerList.LastIndexOf(item, index, count);
        public T[] ToArray() => innerList.ToArray();
        public void TrimExcess() => innerList.TrimExcess();
        public bool TrueForAll(Predicate<T> match) => innerList.TrueForAll(match);
        public int Count => innerList.Count;
        bool ICollection<T>.IsReadOnly => false;
        public IEnumerator<T> GetEnumerator() => ((IEnumerable<T>) innerList).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        
        public int Capacity
        {
            get => innerList.Capacity;
            set => innerList.Capacity = value;
        }

        public int Add(object value)
        {
            var result = ((IList)innerList).Add(value);
            SignalUpdate();
            return result;
        }

        public void Insert(int index, object value)
        {
            ((IList)innerList).Insert(index, value);
            SignalUpdate();
        }

        public void Remove(object value)
        {
            var oldCount = innerList.Count;
            ((IList)innerList).Remove(value);
            if(oldCount != innerList.Count) SignalUpdate();
        }

        object IList.this[int index]
        {
            get => innerList[index];
            set
            {
                innerList[index] = (T) value;
                SignalUpdate();
            }
        }

        public void Add(T item)
        {
            innerList.Add(item);
            SignalUpdate();
        }

        public void AddRange(IEnumerable<T> collection)
        {
            var oldCount = innerList.Count;
            innerList.AddRange(collection);
            if(oldCount != innerList.Count) SignalUpdate();
        }

        public void Clear()
        {
            if (innerList.Count == 0) return;
            innerList.Clear();
            SignalUpdate();
        }

        public void Insert(int index, T item)
        {
            innerList.Insert(index, item);
            SignalUpdate();
        }

        public void InsertRange(int index, IEnumerable<T> collection)
        {
            var oldCount = innerList.Count;
            innerList.InsertRange(index, collection);
            if(oldCount != innerList.Count) SignalUpdate();
        }

        public bool Remove(T item)
        {
            var result = innerList.Remove(item);
            if(result) SignalUpdate();
            return result;
        }

        public int RemoveAll(Predicate<T> match)
        {
            var result = innerList.RemoveAll(match);
            if(result != 0) SignalUpdate();
            return result;
        }

        public void RemoveAt(int index)
        {
            innerList.RemoveAt(index);
            SignalUpdate();
        }

        public void RemoveRange(int index, int count)
        {
            innerList.RemoveRange(index, count);
            if(count > 0) SignalUpdate();
        }

        public void Reverse()
        {
            if (innerList.Count < 2) return;
            innerList.Reverse();
            SignalUpdate();
        }

        public void Reverse(int index, int count)
        {
            if(count < 2) return;
            innerList.Reverse(index, count);
            SignalUpdate();
        }

        public void Sort()
        {
            innerList.Sort();
            SignalUpdate();
        }

        public void Sort(IComparer<T> comparer)
        {
            innerList.Sort(comparer);
            SignalUpdate();
        }

        public void Sort(Comparison<T> comparison)
        {
            innerList.Sort(comparison);
            SignalUpdate();
        }

        public void Sort(int index, int count, IComparer<T> comparer)
        {
            innerList.Sort(index, count, comparer);
            SignalUpdate();
        }

        public T this[int index]
        {
            get => innerList[index];
            set { innerList[index] = value; SignalUpdate();}
        }

    }
}