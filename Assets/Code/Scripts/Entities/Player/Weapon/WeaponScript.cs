using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] private Player _playerScript;

        [SerializeField] private GameObject _muzzleFlash;
        [SerializeField] private ProjectileScript _bullet;

        [SerializeField] private Transform _spawnPoint;


        #region Custom Methods

        public void FireWeapon()
        {
            if (_playerScript.currentState == PlayerStates.Jump)
            {
                return;
            }

            ShowMuzzleFlash();
            FireBullet();
        }

        private void ShowMuzzleFlash()
        {
            GameObject flash = Instantiate(_muzzleFlash, _spawnPoint);
            float muzzleFlashDuration = flash.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy(flash, muzzleFlashDuration);
        }

        private void FireBullet()
        {
            ProjectileScript bulletInstance = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
            bulletInstance.Direction = transform.localScale.x;
        }

        #endregion Custom Methods
    }
}
