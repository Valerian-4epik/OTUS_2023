using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace GameManager
{
    public enum GameState
    {
        None,
        Start,
        Finish,
        Pause,
        Resume,
    }

    public class CoreManager : MonoBehaviour
    {
        [ShowInInspector]
        private List<Listeners.IGameListener> _listeners = new();
        [ShowInInspector]
        private List<Listeners.IGameUpdateListener> _updateListeners = new();
        [ShowInInspector]
        private List<Listeners.IGameFixedUpdateListener> _fixedUpdateListeners = new();
        [ShowInInspector]
        private List<Listeners.IGameLateUpdateListener> _lateUpdateListeners = new();
        
        [ShowInInspector]
        public GameState GameState { get; private set; }

        public void AddListener(Listeners.IGameListener listener)
        {
            _listeners.Add(listener);

            if (listener is Listeners.IGameUpdateListener updateListener)
            {
                _updateListeners.Add(updateListener);
            }
            
            if (listener is Listeners.IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Add(fixedUpdateListener);
            }
            
            if (listener is Listeners.IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Add(lateUpdateListener);
            }
        }

        public void RemoveListener(Listeners.IGameListener listener)
        {
            _listeners.Remove(listener);
            
            if (listener is Listeners.IGameUpdateListener updateListener)
            {
                _updateListeners.Remove(updateListener);
            }
            
            if (listener is Listeners.IGameFixedUpdateListener fixedUpdateListener)
            {
                _fixedUpdateListeners.Remove(fixedUpdateListener);
            }
            
            if (listener is Listeners.IGameLateUpdateListener lateUpdateListener)
            {
                _lateUpdateListeners.Remove(lateUpdateListener);
            }
        }

        [Button]
        public void OnStart()
        {
            foreach (var gameListener in _listeners)
            {
                if (gameListener is Listeners.IGameStartListener startListener)
                {
                    startListener.OnStart();
                }
            }

            Time.timeScale = 1;
            GameState = GameState.Start;
        }

        [Button]
        public void Finish()
        {
            foreach (var gameListener in _listeners)
            {
                if (gameListener is Listeners.IGameFinishListener finishListener)
                {
                    finishListener.OnFinish();
                }
            }

            Time.timeScale = 0;
            GameState = GameState.Finish;
        }

        [Button]
        public void Pause()
        {
            foreach (var gameListener in _listeners)
            {
                if (gameListener is Listeners.IGamePauseListener pauseListener)
                {
                    pauseListener.OnPause();
                }
            }

            Time.timeScale = 0;
            GameState = GameState.Pause;
        }

        [Button]
        public void Resume()
        {
            foreach (var gameListener in _listeners)
            {
                if (gameListener is Listeners.IGameResumeListener resumeListener)
                {
                    resumeListener.OnResume();
                }
            }
            
            Time.timeScale = 1;
            GameState = GameState.Resume;
        }

        private void Update()
        {
            for (int i = 0; i < _updateListeners.Count; i++)
            {
                _updateListeners[i].OnUpdate(Time.deltaTime);
            }
        }

        private void FixedUpdate()
        {
            for (int i = 0; i < _fixedUpdateListeners.Count; i++)
            {
                _fixedUpdateListeners[i].OnFixedUpdate(Time.fixedTime);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < _lateUpdateListeners.Count; i++)
            {
                _lateUpdateListeners[i].OnLateUpdate(Time.deltaTime);
            }
        }
    }
}