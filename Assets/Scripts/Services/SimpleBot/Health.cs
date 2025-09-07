using System;
using UnityEngine;

namespace Assets.Scripts.Services.SimpleBot
{
    [RequireComponent(typeof(Collider))]
    public class Health : MonoBehaviour
    {
        public event Action OnDestroyed;

        [SerializeField] private float _maxHealth = 100f;

        private float _currentHealth;

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage, GameObject source = null)
        {
            _currentHealth -= damage;

            string sourceName = source != null ? source.name : "Unknown";
            Debug.Log(
                $"<color=red>[{gameObject.name}]</color> получил <color=yellow>{damage}</color> урона от <color=cyan>{sourceName}</color>. Остаток здоровья: <color=green>{_currentHealth}</color>");


            if (_currentHealth <= 0f)
            {
                Die();
            }
        }

        private void Die()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}
