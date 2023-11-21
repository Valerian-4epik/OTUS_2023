using Enemy.Agents;
using GameManager;
using Pools;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemySpawner : MonoBehaviour
    {
        [Header("Spawn")] 
        [SerializeField] private GameObject _player;
        [SerializeField] private CoreManager _coreManager;
        [SerializeField] private EnemyPositions _enemyPositions;
        [SerializeField] private MonoPool _enemyPool;

        public GameObject GetEnemy()
        {
            GameObject enemyObject = _enemyPool.Get();
            InitializeEnemy(enemyObject);

            return enemyObject;
        }

        public void Release(GameObject enemy)
        {
            _enemyPool.Release(enemy);
            enemy.transform.SetParent(_enemyPool.Container);
        }

        private void InitializeEnemy(GameObject enemy)
        {
            enemy.transform.SetParent(_enemyPool.WorldTransform);
            Transform spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            Transform attackPosition = _enemyPositions.RandomAttackPosition();
            
            var enemyMoveAgent = enemy.GetComponent<EnemyMoveAgent>();
            var enemyAttackAgent = enemy.GetComponent<EnemyAttackAgent>();
            _coreManager.AddListener(enemyMoveAgent);
            _coreManager.AddListener(enemyAttackAgent);
            enemyMoveAgent.SetDestination(attackPosition.position);
            enemyAttackAgent.SetTarget(_player);
        }
    }
}