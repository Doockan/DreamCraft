using UnityEngine;

namespace Assets.Scripts.Weapon
{
    public class Bullet
    {
        public float Time;

        public Vector3 InitialPosition { get; }
        public Vector3 InitialVelocity { get; }

        public Bullet(Vector3 position, Vector3 velocity)
        {
            InitialPosition = position;
            InitialVelocity = velocity;
            Time = 0f;
        }
    }
}
