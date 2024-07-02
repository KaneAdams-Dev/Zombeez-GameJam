using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private float _xDirection;
        public float Direction
        {
            set => _xDirection = value;
        }

        private float _bulletDropOff = 0.5f;
        public float BulletDropOff
        {
            get => _bulletDropOff;
            set => _bulletDropOff = value;
        }

        private int _damage = 50;

        public int Damage {
            get => _damage; 
            set => _damage = value;
        }

        #region Unity Methods

        private void Awake()
        {
            _rbody = GetComponent<Rigidbody2D>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            transform.localScale = new Vector3(_xDirection, 1, 1);
            Destroy(gameObject, _bulletDropOff);
        }

        private void FixedUpdate()
        {
            _rbody.velocity = 500f * Time.fixedDeltaTime * new Vector2(_xDirection, 0);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out BaseEntity entity))
            {
                entity.TakeDamage(_damage);
            }

            Destroy(gameObject);
        }

        #endregion Unity Methods
    }
}
