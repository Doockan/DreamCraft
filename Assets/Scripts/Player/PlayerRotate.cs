using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerRotate : MonoBehaviour
    {
        [SerializeField] private float _sensitivity = 1f;
        [SerializeField] private float _upDownRange = 90f;

        private Vector2 _lookInput;
        private float _verticalRotation;
        private Camera _mainCamera;

        private void Start()
        {
            _mainCamera = Camera.main;

            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void FixedUpdate()
        {
            if (_lookInput == Vector2.zero) return;

            var mousXRotation = _lookInput.x * _sensitivity;
            transform.Rotate(0, mousXRotation, 0);

            _verticalRotation -= _lookInput.y * _sensitivity;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
            _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0, 0);
        }

        public void OnMousePosition(Vector2 value)
        {
            _lookInput = value;
        }
    }
}
