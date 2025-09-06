using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerRotate _rotate;

        public RaycastWeapon[] RaycastWeapons;

        public Transform FirePoint => _firePoint;
        public PlayerMovement Movement => _movement;
        public PlayerRotate Rotate => _rotate;
    }
}
