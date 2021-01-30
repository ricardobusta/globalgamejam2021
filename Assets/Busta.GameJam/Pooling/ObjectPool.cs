using System;

namespace BustaGames.DataStructures
{
    public class ObjectPool<T> where T : class

    {
        public ObjectPool(T template, int prewarmSize, Action<T> setup = null)
        {

        }

        public T GetObject()
        {
            return null;
        }
    }
}