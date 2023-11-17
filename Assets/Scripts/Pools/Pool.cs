using System.Collections.Generic;
using UnityEngine;

namespace Pools
{
    public abstract class ObjectPool<T> : MonoBehaviour
    {
        [SerializeField] protected T Prefab;
        [SerializeField] protected int Count;
        [SerializeField] protected Transform Container;
        [SerializeField] protected Transform WorldTransform;
        
        protected readonly HashSet<T> ActiveObjects = new();
        
        public abstract T Get();
        
        public abstract void Release(T obj);
    }
}