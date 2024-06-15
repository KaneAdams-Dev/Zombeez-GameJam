using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class BaseEntity : MonoBehaviour
    {
        [Header("Health System")]
        [SerializeField][Min(10)] private int _maxHealth = 100;
        private int _currentHealth;

        [SerializeField] private GameObject _corpsePrefab;
        [SerializeField] private int _defense = 0;

        [Header("Combat System")]
        [SerializeField] private int _attackStrength = 5;

        // Start is called before the first frame update
        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                TakeDamage(10);
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                HealDamage(5);
            }
        }

        private void TakeDamage(int a_damageAmount)
        {
            a_damageAmount -= _defense;
            a_damageAmount = Mathf.Max(0, a_damageAmount);

            _currentHealth -= a_damageAmount;
            OnHealthChange();

            if (_currentHealth <= 0)
            {
                OnDeath();
            }
        }

        private void HealDamage(int a_healAmount)
        {
            _currentHealth += a_healAmount;
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            OnHealthChange();
        }

        private void OnHealthChange()
        {
            Debug.Log("New Health: " + _currentHealth);
        }

        private void OnDeath()
        {
            Destroy(gameObject);
        }
    }
}
