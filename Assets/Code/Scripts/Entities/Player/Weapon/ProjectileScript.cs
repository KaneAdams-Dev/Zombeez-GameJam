using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ProjectileScript : MonoBehaviour
    {
        private Rigidbody2D _rbody;
        private float _xDirection;
        public float Direction
        {
            set => _xDirection = value;
        }

        [SerializeField] private float _bulletDropOff = 0.5f;

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
                entity.TakeDamage(40);
            }

            Destroy(gameObject);
        }

        #endregion Unity Methods
    }
}
