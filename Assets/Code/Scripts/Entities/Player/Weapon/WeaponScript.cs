using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    public class WeaponScript : MonoBehaviour
    {
        [SerializeField] private PlayerScript _playerScript;

        [SerializeField] private GameObject _muzzleFlash;
        [SerializeField] private ProjectileScript _bullet;

        [SerializeField] private Transform _spawnPoint;

        public void FireWeapon()
        {
            if (_playerScript.currentState == PlayerStates.Jump)
            {
                return;
            }

            GameObject flash = Instantiate(_muzzleFlash, _spawnPoint);
            float muzzleFlashDuration = flash.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy(flash, muzzleFlashDuration);

            ProjectileScript bullet = Instantiate(_bullet.gameObject, _spawnPoint.position, Quaternion.identity).GetComponent<ProjectileScript>();
            bullet.Direction = transform.localScale.x;
        }
    }
}
