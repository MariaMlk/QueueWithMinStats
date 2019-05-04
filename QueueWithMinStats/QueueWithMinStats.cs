using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QueueWithMinStats
{
    public class QueueWithMinStats
    {
        #region Properties

        /// <summary>
        /// The capacity of the Elements Collection
        /// </summary>
        private int _capacity;
        public int Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }

        /// <summary>
        /// The number of elements currently in the queue.
        /// </summary>
        private int _length;
        public int Length
        {
            get { return _length; }
            set { _length = value; }
        }

        /// <summary>
        /// The actual data elements stored in the queue.
        /// </summary>
        private int[] _elements;
        protected int[] Elements
        {
            get { return _elements; }
            set { _elements = value; }
        }

        /// <summary>
        /// This is the index where we will dequeue.
        /// </summary>
        private int _frontIndex;
        public int FrontIndex
        {
            get { return _frontIndex; }
            set { _frontIndex = value; }
        }

        /// <summary>
        /// This is the index where we will next enqueue a value. 
        /// It is calculated based on the Front Index, Length, and Capacity
        /// </summary>
        public int BackIndex
        {
            get { return (FrontIndex + Length) % Capacity; }
        }


        public int CurrentMiminum
        {
            get
            {
                if (Length < 1)
                    throw new QueueIsEmptyException("Queue is empty");

                var numbers = new int[Length];
                Array.Copy(Elements, FrontIndex, numbers, 0, Length);
                return numbers.Min();
            }
        }
        #endregion

        #region Constructors

        public QueueWithMinStats()
        {
            Elements = new int[Capacity];
        }

        public QueueWithMinStats(int capacity)
        {
            Capacity = capacity;
            Elements = new int[Capacity];
        }

        #endregion

        #region public methods

        public void Enqueue(int element)
        {
            if (Length == Capacity)
            {
                IncreaseCapacity();
            }
            Elements[BackIndex] = element;
            Length++;
        }

        public int Dequeue()
        {
            if (Length < 1)
            {
                throw new QueueIsEmptyException("Queue is empty");
            }

            var element = Elements[FrontIndex];
            Elements[FrontIndex] = default(int);
            Length--;
            FrontIndex = (FrontIndex + 1) % Capacity;
            return element;
        }

        #endregion

        #region protected methods

        protected void IncreaseCapacity()
        {
            Capacity++;
            Capacity *= 2;
            var tempQueue = new QueueWithMinStats(Capacity);
            while (Length > 0)
            {
                tempQueue.Enqueue(Dequeue());
            }
            Elements = tempQueue.Elements;
            Length = tempQueue.Length;
            FrontIndex = tempQueue.FrontIndex;
        }

        #endregion
    }

    public class QueueIsEmptyException : Exception
    {
        public QueueIsEmptyException()
        {
        }

        public QueueIsEmptyException(string message) : base(message)
        {
        }

        public QueueIsEmptyException(string message, Exception innerException) : base(message, innerException)
        {
        }        
    }
}
