using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public sealed class MonoPool : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private int _count;
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _worldTransform;
        
        private Queue<GameObject> _pool;
        
        public Transform Container => _container;
        public Transform WorldTransform => _worldTransform;

        public void Awake()
        {
            _pool = new Queue<GameObject>(_count);

            for (int i = 0; i < _count; i++)
            {
                _pool.Enqueue(CreateObject());
            }
        }

        public GameObject Get()
        {
            if (_pool.TryDequeue(out GameObject obj))
            {
                return obj;
            }

            return CreateObject();
        }

        public void Release(GameObject obj)
        {
            _pool.Enqueue(obj);
        }

        private GameObject CreateObject()
        {
            return Instantiate(_prefab, _container);
        }
    }
}