using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace KT39_Custom_iterators
{
    public class CircularBuffer<T> : IEnumerable<T>
    {
        private readonly T[] _buffer;
        private int _head;
        private int _tail;
        private int _size;

        public int Capacity => _buffer.Length;
        public int Count => _size;

        public CircularBuffer(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException("Capacity must be greater than zero.", nameof(capacity));

            _buffer = new T[capacity];
            _head = 0;
            _tail = 0;
            _size = 0;
        }

        public void Add(T item)
        {
            if (_buffer.Length == 0) return;

            _buffer[_tail] = item;
            _tail = (_tail + 1) % _buffer.Length;

            if (_size < _buffer.Length)
            {
                _size++;
            }
            else
            {
                _head = (_head + 1) % _buffer.Length;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _size; i++)
            {
                int index = (_head + i) % _buffer.Length;
                yield return _buffer[index];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<T> Reverse()
        {
            for (int i = _size - 1; i >= 0; i--)
            {
                int index = (_head + i) % _buffer.Length;
                yield return _buffer[index];
            }
        }
    }
}
