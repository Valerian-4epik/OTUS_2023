using System;
using UnityEngine;

namespace Inputs
{
    public sealed class KeyboardInput : MonoBehaviour
    {
        public event Action OnShot;
        public event Action<Vector2> OnMoved;

        private Vector2 _leftDirection = new Vector2(-1, 0);
        private Vector2 _rightDirection = new Vector2(1, 0);

        private void Update()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space))
            {
                OnShot?.Invoke();
            }

            if (UnityEngine.Input.GetKey(KeyCode.LeftArrow))
            {
                OnMoved?.Invoke(_leftDirection);
            }
            else if (UnityEngine.Input.GetKey(KeyCode.RightArrow))
            {
                OnMoved?.Invoke(_rightDirection);
            }
        }
    }
}