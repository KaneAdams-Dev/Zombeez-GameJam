using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Managers;
using ZombeezGameJam.Stats;

namespace ZombeezGameJam.Entities.Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform _spawnBoundaryStart;
        [SerializeField] private Transform _spawnBoundaryEnd;

        [SerializeField] private BaseEntityStats[] enemyVariants;

        [SerializeField] private Zombie _enemyPrefab;

        [SerializeField] private Vector2Int _spawnRange;

        [SerializeField] private bool _spawnFromStart;

        // Start is called before the first frame update
        void Start()
        {
            if (_spawnFromStart)
            {
                StartCoroutine(NormalSpawnRoutine());
            }
        }

        private void OnEnable()
        {
            GameManager.OnFinalWaveBegins += StartFinalWave;
        }

        private void OnDisable()
        {
            GameManager.OnFinalWaveBegins -= StartFinalWave;
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    StopAllCoroutines();
                
            //}
        }

        

        void StartFinalWave()
        {
            StartCoroutine(FinalWaveRoutine());
        }

        private void SpawnZombies()
        {
            float randomX = Random.Range(_spawnBoundaryStart.position.x, _spawnBoundaryEnd.position.x);
            Vector3 spawnPoint = new Vector3(randomX, -0.2f, 0f);
            Zombie spawnedZombie = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
            spawnedZombie.Stats = enemyVariants[Random.Range(0, enemyVariants.Length)];
            spawnedZombie.patrolStartPosition = _spawnBoundaryStart;
            spawnedZombie.patrolEndPosition = _spawnBoundaryEnd;
        }

        IEnumerator NormalSpawnRoutine()
        {
            while (true)
            {
                SpawnZombies();

                yield return new WaitForSeconds(23);

                //Vector3 spawnPoint = new Vector3(_spawnBoundaryStart.position.x, -0.2f, 0f);
                //Zombie spawnedZombie = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
                //spawnedZombie.Stats = enemyVariants[Random.Range(0, enemyVariants.Length)];
                //spawnedZombie.patrolStartPosition = _spawnBoundaryStart;
                //spawnedZombie.patrolEndPosition = _spawnBoundaryEnd;

                //spawnedZombie.UpdateZombieState(ZombieStates.Spawn);
            }
        }

        IEnumerator FinalWaveRoutine()
        {
            while (GameManager.instance.IsFinalWave)
            {
                int spawnAmount = Random.Range(_spawnRange.x, _spawnRange.y);

                for (int i = 0; i < spawnAmount; i++)
                {
                    SpawnZombies();

                    //float randomX = Random.Range(_spawnBoundaryStart.position.x, _spawnBoundaryEnd.position.x);
                    //Vector3 spawnPoint = new Vector3(randomX, -0.2f, 0f);
                    //Zombie spawnedZombie = Instantiate(_enemyPrefab, spawnPoint, Quaternion.identity);
                    //spawnedZombie.Stats = enemyVariants[Random.Range(0, enemyVariants.Length)];
                    //spawnedZombie.patrolStartPosition = _spawnBoundaryStart;
                    //spawnedZombie.patrolEndPosition = _spawnBoundaryEnd;

                    //spawnedZombie.UpdateZombieState(ZombieStates.Spawn);

                    yield return null;
                }
                
                yield return new WaitForSeconds(5);
            }
        }
    }
}
