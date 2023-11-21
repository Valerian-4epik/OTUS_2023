using Character;
using Components;
using GameManager;
using UnityEngine;

namespace Inputs
{
    public sealed class InputController : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener,
        Listeners.IGamePauseListener,
        Listeners.IGameResumeListener
    {
        [SerializeField] private KeyboardInput _keyboardInput;
        [SerializeField] private MoveComponent _moveComponent;
        [SerializeField] private CharacterShooter _characterShooter;

        public void OnStart()
        {
            Subscribe();
        }

        public void OnFinish()
        {
            Unsubscribe();
        }

        public void OnPause()
        {
            Unsubscribe();
        }

        public void OnResume()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _keyboardInput.OnMoved += _moveComponent.Move;
            _keyboardInput.OnShot += _characterShooter.Shoot;
        }

        private void Unsubscribe()
        {
            _keyboardInput.OnMoved -= _moveComponent.Move;
            _keyboardInput.OnShot -= _characterShooter.Shoot;
        }
    }
}