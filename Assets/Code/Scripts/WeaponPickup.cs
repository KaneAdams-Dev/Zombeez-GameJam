using UnityEngine;
using ZombeezGameJam.Entities.Player;

namespace ZombeezGameJam
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private PlayerWeapons _droppedWeapon;

        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        [SerializeField] private float _bopSpeed;

        private void Start()
        {
            _minY = transform.position.y - 0.05f;
            _maxY = transform.position.y + 0.05f;

            Vector3 initialPosition = new Vector3(transform.position.x, Random.Range(_minY, _maxY), transform.position.z);

            transform.position = initialPosition;
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
            if (collision.gameObject.TryGetComponent(out PlayerScript playerScript))
            {
                playerScript.currentWeapon = _droppedWeapon;
                Destroy(gameObject);
            }
        }
    }
}
