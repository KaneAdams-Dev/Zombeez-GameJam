using UnityEngine;
using ZombeezGameJam.Entities.Player;

namespace ZombeezGameJam.Weapons
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] private PlayerWeapons _droppedWeapon;

        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        [SerializeField] private float _bopSpeed;

        #region Unity Methods

        private void Start()
        {
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
                player.EquipWeapon(_droppedWeapon);
                //Player
                Destroy(gameObject);
            }
        }

        //public void Interact()
        //{
        //    //Player player = FindObjectOfType<Player>();
        //    //player.currentWeapon = _droppedWeapon;
        //    //
        //    ////a_player.currentWeapon = _droppedWeapon;
        //    ///
        //    FindObjectOfType<Player>().EquipWeapon(_droppedWeapon);
        //    Destroy(gameObject);
        //}

        #endregion Unity Methods
    }
}
