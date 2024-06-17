using UnityEngine;

namespace ZombeezGameJam.Entities
{
    public class BaseEntity : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _renderer;

        [Header("Health System")]
        [SerializeField][Min(10)] private int _maxHealth = 100;
        [SerializeField] private int _defense = 0;

        [Header("Combat System")]
        [SerializeField] private int _attackStrength = 5;

        [Space(5)]
        [SerializeField] private GameObject _corpsePrefab;
        
        private int _currentHealth;

        #region Unity Methods

        private void Awake()
        {
            //_renderer = GetComponentInChildren<SpriteRenderer>();
        }

        // Start is called before the first frame update
        public virtual void Start()
        {
            _currentHealth = _maxHealth;
            ReturnToDefaultColour();
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

        #endregion Unity Methods

        #region Custom Methods

        public void TakeDamage(int a_damageAmount)
        {
            a_damageAmount -= _defense;
            a_damageAmount = Mathf.Max(0, a_damageAmount);

            if (a_damageAmount > 0)
            {
                _renderer.color = Color.red;
                Invoke(nameof(ReturnToDefaultColour), 0.1f);
            }

            _currentHealth -= a_damageAmount;
            OnHealthChange();

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

            OnHealthChange();
        }

        private void OnHealthChange()
        {
            Debug.Log("New Health: " + _currentHealth);
        }

        private void ReturnToDefaultColour()
        {
            _renderer.color = Color.white;
        }

        public void OnDeath()
        {
            if (_corpsePrefab != null)
            {
                Instantiate(_corpsePrefab, transform.position, Quaternion.identity);
            }
            Destroy(gameObject);
        }

        #endregion Custom Methods
    }
}
