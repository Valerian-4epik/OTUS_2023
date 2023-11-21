using System.Collections.Generic;
using Components;
using GameManager;
using Level;
using Pools;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : MonoBehaviour,
        Listeners.IGameFixedUpdateListener
    {
        [SerializeField] private LevelBounds _levelBounds;
        [SerializeField] private MonoPool _bulletPool;

        private readonly HashSet<Bullet> _activeBullets = new();
        private readonly List<Bullet> _bulletCache = new();
        
        public void OnFixedUpdate(float fixedTimeDelta)
        {
            _bulletCache.Clear();
            _bulletCache.AddRange(_activeBullets);
            
            for (int i = 0, count = _bulletCache.Count; i < count; i++)
            {
                Bullet bullet = _bulletCache[i];
            
                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    Release(bullet);
                }
            }
        }

        public void Shoot(Args args)
        {
            GameObject bulletObject = _bulletPool.Get();
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            bullet.transform.SetParent(_bulletPool.WorldTransform);
            bullet.Init(args.Position, args.Velocity, args.PhysicsLayer, args.Color, args.Damage, args.IsPlayer);
            bullet.CollisionEntered += BulletCollision;
        }

        public Bullet GetBullet()
        {
            var bulletObject = _bulletPool.Get();
            Bullet bullet = bulletObject.GetComponent<Bullet>();
            return bullet;
        }

        private void Release(Bullet bullet)
        {
            bullet.CollisionEntered -= BulletCollision;
            bullet.transform.SetParent(_bulletPool.Container);
        }

        private void BulletCollision(Bullet bullet, GameObject collisionObject)
        {
            if (!collisionObject.TryGetComponent(out TeamComponent team))
            {
                return;
            }

            if (bullet.IsPlayer == team.IsPlayer)
            {
                return;
            }

            if (collisionObject.TryGetComponent(out HitPointsComponent hitPoints))
            {
                hitPoints.TakeDamage(bullet.Damage);
            }

            Release(bullet);
        }

        public struct Args
        {
            public Vector2 Position;
            public Vector2 Velocity;
            public Color Color;
            public int PhysicsLayer;
            public int Damage;
            public bool IsPlayer;
        }
    }
}