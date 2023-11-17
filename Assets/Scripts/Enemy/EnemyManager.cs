using System.Collections;
using Bullets;
using Components;
using Enemy.Agents;
using UnityEngine;

namespace Enemy
{
    public sealed class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyPool _enemyPool;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;

        private IEnumerator Start()
        {
            float delay = 1;

            while (enabled)
            {
                yield return new WaitForSeconds(delay);

                Enemy enemy = _enemyPool.Get();

                enemy.GetComponent<HitPointsComponent>().OnHealthPointsDepleted += OnDestroyed;
                enemy.GetComponent<EnemyAttackAgent>().OnFire += OnFire;
            }
        }

        private void OnDestroyed(GameObject enemy)
        {
            enemy.GetComponent<HitPointsComponent>().OnHealthPointsDepleted -= OnDestroyed;
            enemy.GetComponent<EnemyAttackAgent>().OnFire -= OnFire;
            
            _enemyPool.Release(enemy.GetComponent<Enemy>());
        }

        private void OnFire(GameObject enemy, Vector2 position, Vector2 direction)
        {
            float speed = 2.0f;
            
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
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