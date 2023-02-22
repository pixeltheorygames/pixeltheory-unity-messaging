using System.Collections.Concurrent;
using UnityEngine;


namespace Pixeltheory
{
    public class ObjectPool<T> where T : IPoolable, new()
    {
        #region Fields
        #region Private
        private ConcurrentBag<T> pool;
        #endregion //Private
        #endregion //Fields

        #region Constructor
        public ObjectPool(int initialAmount = 0)
        {
            this.pool = new ConcurrentBag<T>();
            for (int i = 0; i < initialAmount; i++)
            {
                this.pool.Add(new T());
            }
        }
        #endregion //Constructor

        #region Methods
        #region Public
        public T Borrow()
        {
            T lendable;
            if (!this.pool.TryTake(out lendable))
            {
                lendable = new T();
            }
            return lendable;
        }

        public void Return(T lendable)
        {
            lendable.Reset();
            this.pool.Add(lendable);
        }
        #endregion //Public
        #endregion //Methods
    }
}