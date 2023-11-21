using Components;
using GameManager;
using UnityEngine;

namespace Enemy.Agents
{
    public sealed class EnemyMoveAgent : MonoBehaviour,
        Listeners.IGameFixedUpdateListener
    {
        private const float EPSILON = 0.25f;

        [SerializeField] private MoveComponent _moveComponent;

        private Vector2 _destination;

        public bool IsReached { get; private set; }

        public void SetDestination(Vector2 endPoint)
        {
            _destination = endPoint;
            IsReached = false;
        }
        
        public void OnFixedUpdate(float fixedTimeDelta)
        {
            if (IsReached)
            {
                return;
            }

            Vector2 vector = _destination - (Vector2)transform.position;

            if (vector.magnitude <= EPSILON)
            {
                IsReached = true;
                return;
            }

            Vector2 direction = vector.normalized * Time.fixedDeltaTime;
            _moveComponent.Move(direction);
        }
    }
}