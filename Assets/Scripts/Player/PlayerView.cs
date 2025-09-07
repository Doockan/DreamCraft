using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerRotate _rotate;
        [SerializeField] private ActiveWeapon _activeWeapon;
        [SerializeField] private Camera _playerCamera;

        public Transform FirePoint => _firePoint;
        public PlayerMovement Movement => _movement;
        public PlayerRotate Rotate => _rotate;
        public ActiveWeapon ActiveWeapon => _activeWeapon;
        public Camera Camera => _playerCamera;
    }
}
