using System.Collections;
using GameManager;
using UnityEngine;

namespace Enemy
{
    public class EnemyTimeSpawner : MonoBehaviour,
        Listeners.IGameStartListener,
        Listeners.IGameFinishListener
    {
        [SerializeField] private EnemyManager _enemyManager;
        [SerializeField] private float _delay = 1f;

        private Coroutine _spawning;
        
        public void OnStart()
        {
            _spawning = StartCoroutine(Spawning());
        }

        public void OnFinish()
        {
            StopCoroutine(_spawning);
        }
        
        private IEnumerator Spawning()
        {
            while (enabled)
            {
                yield return new WaitForSeconds(_delay);

                _enemyManager.SpawnEnemy();
            }
        }
    }
}