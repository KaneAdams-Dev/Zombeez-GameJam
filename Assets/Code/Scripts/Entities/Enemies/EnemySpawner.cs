using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnBoundaryStart;
        [SerializeField] private Transform _spawnBoundaryEnd;

        [SerializeField] private BaseEntityStats[] enemyVariants;

        [SerializeField] private Zombie _enemyPrefab;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                StopAllCoroutines();
                StartCoroutine(FinalWaveRoutine());
            }
        }

        IEnumerator FinalWaveRoutine()
        {
            while (/*GameManager.instance.IsFinalWave*/true)
            {
                yield return new WaitForSeconds(5);

                int spawnAmount = Random.Range(1, 5);

                for (int i = 0; i < spawnAmount; i++)
                {
                    float randomX = Random.Range(_spawnBoundaryStart.position.x, _spawnBoundaryEnd.position.x);
                    Vector3 spawnPoint = new Vector3(randomX, -0.2f, 0f);
                    Zombie spawnedZombie = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
                    spawnedZombie.Stats = enemyVariants[Random.Range(0, enemyVariants.Length)];
                    spawnedZombie.patrolStartPosition = _spawnBoundaryStart;
                    spawnedZombie.patrolEndPosition = _spawnBoundaryEnd;

                    yield return null;
                }

            }
        }
    }
}
