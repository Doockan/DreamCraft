using UnityEngine;

namespace Assets.Scripts.Services.SimpleBot
{
    public class SimpleBotAI : MonoBehaviour
    {
        [Header("Movement")] [SerializeField] private float _moveSpeed = 3f;
        [SerializeField] private float _rotationSpeed = 5f;

        [Header("Combat")] [SerializeField] private float _damage = 10f;
        [SerializeField] private float _attackRange = 2f;
        [SerializeField] private float _attackCooldown = 1.5f;
        private Transform _target;
        private float _attackTimer;

        public void Initialize(Transform playerTransform)
        {
            _target = playerTransform;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_target == null) return;

            MoveTowardsTarget();
            TryAttack();
        }

        private void MoveTowardsTarget()
        {
            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0f;

            transform.position += direction * _moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    _rotationSpeed * Time.deltaTime
                );
            }
        }

        private void TryAttack()
        {
            _attackTimer -= Time.deltaTime;

            float distance = Vector3.Distance(transform.position, _target.position);
            if (distance <= _attackRange && _attackTimer <= 0f)
            {
                var health = _target.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(_damage, gameObject);
                    Debug.Log(
                        $"<color=cyan>[{gameObject.name}]</color> атаковал <color=red>[{_target.name}]</color> и нанес <color=yellow>{_damage}</color> урона."
                    );
                }

                _attackTimer = _attackCooldown;
            }
        }
    }
}
