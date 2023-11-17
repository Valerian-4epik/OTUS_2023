using Components;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour
    {
        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;
        private bool _isReached;
        private float _epsilon = 0.25f;
        
        public bool IsReached => _isReached;

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            _isReached = false;
        }

        private void FixedUpdate()
        {
            if (_isReached)
            {
                return;
            }

            Vector2 vector = _destination - (Vector2)transform.position;

            if (vector.magnitude <= _epsilon)
            {
                _isReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(direction);
        }
    }
}