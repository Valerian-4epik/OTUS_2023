using System;
using GameManager;
using UnityEngine;

namespace Inputs
{
    public sealed class KeyboardInput : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener,
        Listeners.IGamePauseListener,
        Listeners.IGameResumeListener,
        Listeners.IGameUpdateListener
    {
        public event Action OnShot;
        public event Action<Vector2> OnMoved;

        private Vector2 _leftDirection = new Vector2(-1, 0);
        private Vector2 _rightDirection = new Vector2(1, 0);

        public void OnUpdate(float timeDelta)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                OnShot?.Invoke();
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                OnMoved?.Invoke(_leftDirection);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                OnMoved?.Invoke(_rightDirection);
            }
        }

        public void OnStart()
        {
            enabled = true;
        }

        public void OnFinish()
        {
            enabled = false;
        }

        public void OnPause()
        {
            enabled = false;
        }

        public void OnResume()
        {
            enabled = true;
        }
    }
}