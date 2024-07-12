using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities.Player
{
    public class WeaponHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;

        [SerializeField] private GameObject _muzzleFlash;
        [SerializeField] private Projectile _bullet;

        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private int _damage;

        private float _fireRate;

        private float _bulletDropoff;

        private bool _canFire;
        private bool _isFiring;

        private bool _isAutomatic;

        private AudioClip _gunShotAudio;

        // Start is called before the first frame update
        void Start()
        {
            _canFire = true;
            _isFiring = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (_isFiring && _isAutomatic && _player.inputScript.CanPlayerFire())
            {
                Fire();
            }
        }

        public void EquipWeapon(WeaponStats a_newWeaponStats)
        {
            _muzzleFlash = a_newWeaponStats.MuzzleFlash;
            _bullet = a_newWeaponStats.BulletPrefab.GetComponent<Projectile>();
            _damage = a_newWeaponStats.Damage;
            _bulletDropoff = a_newWeaponStats.BulletDropoff * 0.1f;
            _fireRate = a_newWeaponStats.FireRate;
            _isAutomatic = a_newWeaponStats.IsAutomatic;
            _gunShotAudio = a_newWeaponStats.GunShotAudio;
        }

        internal void Fire()
        {
            //Debug.Log("Firing Started");
            if (_canFire)
            {
                _canFire = false;

                ShowMuzzleFlash();
                FireBullet();
                SoundFXManager.instance.PlaySoundClip(_gunShotAudio, _spawnPoint, 1, 64);

                StartCoroutine(FireRateHandler());
            }
        }

        internal void SprayFire()
        {
            if (_canFire)
            {
                _canFire = false;

                ShowMuzzleFlash();
                FireBullet();

                StartCoroutine(FireRateHandler());
            }
        }

        internal void StartFiring()
        {
            _isFiring = true;
        }

        internal void StopFiring()
        {
            //Debug.Log("Firing Ended");
            _isFiring = false;
        }

        private void ShowMuzzleFlash()
        {
            GameObject flash = Instantiate(_muzzleFlash, _spawnPoint);
            float muzzleFlashDuration = flash.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
            Destroy(flash, muzzleFlashDuration);
        }

        private void FireBullet()
        {
            Projectile bulletInstance = Instantiate(_bullet, _spawnPoint.position, Quaternion.identity);
            bulletInstance.Damage = _damage;
            bulletInstance.BulletDropOff = _bulletDropoff;
            bulletInstance.Direction = transform.localScale.x;
        }

        private IEnumerator FireRateHandler()
        {
            float timeToNextFire = 1 / _fireRate;
            yield return new WaitForSeconds(timeToNextFire);
            _canFire = true;
        }
    }
}
