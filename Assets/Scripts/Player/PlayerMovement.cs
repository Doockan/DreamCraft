using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _speed = 5f;

        private Vector2 _moveInput;

        private void FixedUpdate()
        {
            if (_moveInput == Vector2.zero) return;

            Vector3 forward = transform.forward;
            forward.y = 0;
            forward.Normalize();

            Vector3 right = transform.right;
            right.y = 0;
            right.Normalize();

            Vector3 movement = forward * _moveInput.y + right * _moveInput.x;

            transform.position += movement * Time.deltaTime * _speed;
        }

        public void OnMove(Vector2 value)
        {
            _moveInput = value;
        }
    }
}
