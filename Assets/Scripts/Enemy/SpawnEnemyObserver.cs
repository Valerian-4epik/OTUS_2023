using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class SpawnEnemyObserver : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        [SerializeField] private EnemyPool _enemyPool;

        private void OnEnable()
        {
            _enemyPool.OnEnemySpawned += OnEnemySpawned;
        }
        
        private void OnDisable()
        {
            _enemyPool.OnEnemySpawned -= OnEnemySpawned;
        }

        private void OnEnemySpawned(Enemy enemy)
        {
            enemy.GetComponent<EnemyAttackAgent>().SetTarget(_player);
        }
    }
}