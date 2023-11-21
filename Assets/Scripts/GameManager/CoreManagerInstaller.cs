using System;
using UnityEngine;

namespace GameManager
{
    [RequireComponent(typeof(CoreManager))]
    public class CoreManagerInstaller : MonoBehaviour
    {
        private void Awake()
        {
            var coreManager = GetComponent<CoreManager>();
            var listeners = GetComponentsInChildren<Listeners.IGameListener>();

            foreach (var listener in listeners)
            {
                coreManager.AddListener(listener);
            }
        }
    }
}