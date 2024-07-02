using System.Collections;
using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Weapons
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private WeaponStats _stats;

        private WeaponPickup _spawnedWeapon;

        #region Unity Methods

        // Start is called before the first frame update
        private void Start()
        {
            SpawnGun();
            StartCoroutine(SpawnGunRoutine());
        }

        #endregion Unity Methods

        #region Custom Methods

        private void SpawnGun()
        {
            _spawnedWeapon = Instantiate(_weaponPrefab, transform.position, Quaternion.identity).GetComponent<WeaponPickup>();
            _spawnedWeapon.DroppedWeapon = _stats;
        }

        #endregion Custom Methods

        #region Coroutines

        private IEnumerator SpawnGunRoutine()
        {
            while (true)
            {
                if (_spawnedWeapon == null)
                {
                    yield return new WaitForSeconds(10);

                    SpawnGun();
                }

                yield return null;
            }
        }

        #endregion Coroutines
    }
}
