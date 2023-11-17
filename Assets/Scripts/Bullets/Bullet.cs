using System;
using UnityEngine;

namespace Bullets
{
    public sealed class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public int Damage { get; private set; }
        public bool IsPlayer { get; private set; }

        public event Action<Bullet, GameObject> CollisionEntered;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            CollisionEntered?.Invoke(this, collision.gameObject);
        }

        public void Init(Vector3 position, Vector2 velocity, int physicsLayer, Color color, int damage, bool isPlayer)
        {
            SetPosition(position);
            SetVelocity(velocity);
            SetPhysicsLayer(physicsLayer);
            SetColor(color);
            SetDamage(damage);
            SetIsPlayer(isPlayer);
        }

        private void SetVelocity(Vector2 velocity)
        {
            _rigidbody2D.velocity = velocity;
        }

        private void SetPhysicsLayer(int physicsLayer)
        {
            gameObject.layer = physicsLayer;
        }

        private void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        private void SetColor(Color color)
        {
            _spriteRenderer.color = color;
        }

        private void SetDamage(int damage)
        {
            Damage = damage;
        }

        private void SetIsPlayer(bool isPlayer)
        {
            IsPlayer = isPlayer;
        }
    }
}