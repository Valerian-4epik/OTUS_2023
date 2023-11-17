using Components;
using UnityEngine;

namespace GameManager
{
    public sealed class KillCharacterObserver : MonoBehaviour
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private GameManager _gameManager;

        private void OnEnable()
        {
            _hitPointsComponent.OnHealthPointsDepleted += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            _hitPointsComponent.OnHealthPointsDepleted -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject obj)
        {
            _gameManager.FinishGame();
        }
    }
}