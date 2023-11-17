using System;
using Enemy.Agents;
using Pools;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyPool : ObjectPool<Enemy>
    {
        [Header("Spawn")] 
        [SerializeField] private EnemyPositions _enemyPositions;

        private MonoPool<Enemy> _enemyPool;

        public event Action<Enemy> OnEnemySpawned;

        private void Awake()
        {
            _enemyPool = new MonoPool<Enemy>(Prefab, Count, Container);
        }
        
        public override Enemy Get()
        {
            Enemy enemy = _enemyPool.Get();
            InitializeEnemy(enemy);

            return enemy;
        }

        public override void Release(Enemy enemy)
        {
            _enemyPool.Release(enemy);
            enemy.transform.SetParent(Container);
        }

        private void InitializeEnemy(Enemy enemy)
        {
            enemy.transform.SetParent(WorldTransform);
            Transform spawnPosition = _enemyPositions.RandomSpawnPosition();
            enemy.transform.position = spawnPosition.position;
            Transform attackPosition = _enemyPositions.RandomAttackPosition();
            enemy.GetComponent<EnemyMoveAgent>().SetDestination(attackPosition.position);

            OnEnemySpawned?.Invoke(enemy);
        }
    }
}