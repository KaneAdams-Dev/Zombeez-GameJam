using System.Collections;
using UnityEngine;

namespace ZombeezGameJam.Weapons
{
    public class WeaponSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _gunToSpawn;

        private GameObject _spawnedItem;

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
            _spawnedItem = Instantiate(_gunToSpawn, transform.position, Quaternion.identity);
        }

        #endregion Custom Methods

        #region Coroutines

        private IEnumerator SpawnGunRoutine()
        {
            while (true)
            {
                if (_spawnedItem == null)
                {
                    yield return new WaitForSeconds(30);

                    SpawnGun();
                }

                yield return null;
            }
        }

        #endregion Coroutines
    }
}
