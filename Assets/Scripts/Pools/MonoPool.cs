using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public sealed class MonoPool<T> where T : MonoBehaviour
    {
        private readonly T _prefab;
        private readonly Transform _container;
        private readonly Queue<T> _pool;

        public MonoPool(T prefab, int count, Transform container)
        {
            _prefab = prefab;
            _container = container;
            _pool = new Queue<T>(count);

            for (int i = 0; i < count; i++)
            {
                _pool.Enqueue(CreateObject());
            }
        }

        public T Get()
        {
            if (_pool.TryDequeue(out T obj))
            {
                return obj;
            }

            return CreateObject();
        }

        public void Release(T obj)
        {
            _pool.Enqueue(obj);
        }

        private T CreateObject()
        {
            return Object.Instantiate(_prefab, _container);
        }
    }
}