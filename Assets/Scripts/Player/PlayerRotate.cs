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

        public void OnMousePosition(Vector2 value)
        {
            _lookInput = value;
        }

        private void Update()
        {
            float targetX = _lookInput.x * _sensitivity;
            float targetY = _lookInput.y * _sensitivity;

            transform.Rotate(0f, targetX * Time.deltaTime * 60f, 0f);
            _verticalRotation -= targetY * Time.deltaTime * 60f;
            _verticalRotation = Mathf.Clamp(_verticalRotation, -_upDownRange, _upDownRange);
            _mainCamera.transform.localRotation = Quaternion.Euler(_verticalRotation, 0f, 0f);

            _lookInput = Vector2.zero;
        }
    }
}
