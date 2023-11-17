using System.Collections.Generic;
using Components;
using Level;
using Pools;
using UnityEngine;

namespace Bullets
{
    public sealed class BulletSystem : ObjectPool<Bullet>
    {
        [SerializeField] private LevelBounds _levelBounds;

        private readonly List<Bullet> _bulletCache = new();
        private MonoPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = new MonoPool<Bullet>(Prefab, Count, Container);
        }

        private void FixedUpdate()
        {
            _bulletCache.Clear();
            _bulletCache.AddRange(ActiveObjects);

            for (int i = 0, count = _bulletCache.Count; i < count; i++)
            {
                Bullet bullet = _bulletCache[i];

                if (!_levelBounds.InBounds(bullet.transform.position))
                {
                    Release(bullet);
                }
            }
        }

        public override void Release(Bullet bullet)
        {
            bullet.CollisionEntered -= BulletCollision;
            bullet.transform.SetParent(Container);
        }

        public void FlyBulletByArgs(Args args)
        {
            Bullet bullet = _bulletPool.Get();
            bullet.transform.SetParent(WorldTransform);
            bullet.Init(args.Position, args.Velocity, args.PhysicsLayer, args.Color, args.Damage, args.IsPlayer);
            bullet.CollisionEntered += BulletCollision;
        }

        public override Bullet Get()
        {
            return _bulletPool.Get();
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