using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities
{
    public class BaseEntity : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] protected internal BaseEntityStats _stats;
        [SerializeField] private SpriteRenderer _renderer;

        [Header("Health System")]
        [SerializeField][Min(10)] private int _maxHealth = 100;
        [SerializeField] private int _defense = 0;

        [Header("Combat System")]
        [SerializeField] private int _attackStrength = 5;

        public int AttackStength => _attackStrength;

        [Space(5)]
        [SerializeField] private GameObject _corpsePrefab;

        private int _currentHealth;

        private float movementSpeed;

        public float MovementSpeed
        {
            get => movementSpeed;
            set => movementSpeed = value;
        }

        public int CurrentHealth
        {
            get => _currentHealth;
            set => _currentHealth = value;
        }

        public int MaxHealth
        {
            get => _maxHealth;
            set => _maxHealth = value;
        }

        #region Unity Methods

        // Start is called before the first frame update
        public virtual void Start()
        {
            ApplyEntityStats();
            _currentHealth = _maxHealth;
            ReturnToDefaultColour();
        }

        #endregion Unity Methods

        #region Custom Methods

        public virtual void ApplyEntityStats()
        {
            _maxHealth = _stats.MaxHealth;
            _defense = _stats.Defense;
            _corpsePrefab = _stats.CorpsePrefab;

            movementSpeed = _stats.MovementSpeed;
        }

        public virtual void TakeDamage(int a_damageAmount)
        {
            a_damageAmount -= _defense;
            a_damageAmount = Mathf.Max(0, a_damageAmount);

            if (a_damageAmount > 0)
            {
                _renderer.color = Color.red;
                Invoke(nameof(ReturnToDefaultColour), 0.1f);
            }

            _currentHealth -= a_damageAmount;
            UpdateHealth();

            if (_currentHealth <= 0)
            {
                OnDeath();
            }
        }

        public void HealDamage(int a_healAmount)
        {
            _currentHealth += a_healAmount;
            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }

            UpdateHealth();
        }

        public virtual void UpdateHealth()
        {
            Debug.Log("New Health: " + _currentHealth);
        }

        private void ReturnToDefaultColour()
        {
            _renderer.color = Color.white;
        }

        public virtual void OnDeath()
        {
            if (_corpsePrefab != null)
            {
                GameObject corpse = Instantiate(_corpsePrefab, transform.position, Quaternion.identity);
                corpse.transform.localScale = transform.localScale;
            }

            Destroy(gameObject);
        }

        public virtual void OnFall()
        {
            Destroy(gameObject);
        }

        #endregion Custom Methods
    }
}
