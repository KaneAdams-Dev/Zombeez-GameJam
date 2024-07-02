using UnityEngine;
using ZombeezGameJam.Entities.Player;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Weapons
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private WeaponStats _droppedWeapon;
        public WeaponStats DroppedWeapon
        {
            get => _droppedWeapon;
            set => _droppedWeapon = value;
        }

        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        [SerializeField] private float _bopSpeed;

        private SpriteRenderer _spriteRenderer;

        #region Unity Methods

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Start()
        {
            _spriteRenderer.sprite = _droppedWeapon.PickupSprite;

            _minY = transform.localPosition.y - 0.05f;
            _maxY = transform.localPosition.y + 0.05f;

            Vector3 initialPosition = new Vector3(transform.localPosition.x, Random.Range(_minY, _maxY), transform.localPosition.z);
            transform.localPosition = initialPosition;
        }

        private void FixedUpdate()
        {
            if (transform.localPosition.y >= _maxY || transform.localPosition.y <= _minY)
            {
                _bopSpeed *= -1.0f;
            }

            transform.localPosition += new Vector3(0f, _bopSpeed, 0f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out Player player))
            {
                collision.gameObject.GetComponent<Player>().PickUpWeapon(_droppedWeapon);

                Destroy(gameObject);
            }
        }

        #endregion Unity Methods
    }
}
