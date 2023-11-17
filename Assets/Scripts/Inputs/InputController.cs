using Character;
using Components;
using UnityEngine;

namespace Inputs
{
    public sealed class InputController : MonoBehaviour
    {
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CharacterShootListener _characterShootListener;

        private void OnEnable()
        {
            _keyboardInput.OnMoved += _moveComponent.Move;
            _keyboardInput.OnShot += _characterShootListener.Shoot;
        }
    }
}