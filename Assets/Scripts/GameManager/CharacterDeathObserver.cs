using Components;
using UnityEngine;

namespace GameManager
{
    public sealed class CharacterDeathObserver : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener
    {
        [SerializeField] private HitPointsComponent _hitPointsComponent;
        [SerializeField] private CoreManager _coreManager;

        public void OnStart()
        {
            _hitPointsComponent.OnHealthPointsDepleted += this.OnCharacterDeath;
        }

        public void OnFinish()
        {
            _hitPointsComponent.OnHealthPointsDepleted -= this.OnCharacterDeath;
        }

        private void OnCharacterDeath(GameObject obj)
        {
            _coreManager.Finish();
        }
    }
}