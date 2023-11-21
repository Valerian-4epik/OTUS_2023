using Bullets;
using Components;
using UnityEngine;

namespace Character
{
    public sealed class CharacterShooter : MonoBehaviour
    {
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        [SerializeField] private WeaponComponent _weaponComponent;
        
        public void Shoot()
        {
            _bulletSystem.Shoot(new BulletSystem.Args
            {
                IsPlayer = true,
                PhysicsLayer = (int)_bulletConfig.PhysicsLayer,
                Color = _bulletConfig.Color,
                Damage = _bulletConfig.Damage,
                Position = _weaponComponent.Position,
                Velocity = _weaponComponent.Rotation * Vector3.up * _bulletConfig.Speed
            });
        }
    }
}