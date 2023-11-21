using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public void SpawnEnemy()
        {
            GameObject enemy = _enemySpawner.GetEnemy();

            enemy.GetComponent<HitPointsComponent>().OnHealthPointsDepleted += OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHealthPointsDepleted -= OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
            
            _enemySpawner.Release(enemy);
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            float speed = 2.0f;
            
            _bulletSystem.Shoot(new BulletSystem.Args
            {
                IsPlayer = false,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = position,
                Velocity = direction * speed
            });
        }
    }
}