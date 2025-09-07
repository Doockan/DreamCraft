using UnityEngine;

namespace Assets.Scripts.Services.SimpleBot
{
    public class SimpleBotAI : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float rotationSpeed = 5f;
        private Transform _target;

        public void Initialize(Transform playerTransform)
        {
            _target = playerTransform;
        }

        private void Update()
        {
            if (_target == null) return;

            Vector3 direction = (_target.position - transform.position).normalized;
            direction.y = 0f;

            transform.position += direction * moveSpeed * Time.deltaTime;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}
