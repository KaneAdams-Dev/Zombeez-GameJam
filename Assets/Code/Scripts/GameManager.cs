using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private List<GameObject> _survivorsInHorde;
        [SerializeField] private GameObject _playerToSpawn;         // TEMP 

        public static event Action<GameObject> OnPlayerRespawned;
        public static event Action OnFinalWaveBegins;
        public static event Action<int> OnSurvivorCountUpdate;
        public static event Action<int> OnZombieCountUpdate;
        public static event Action<int> OnCountdownUpdate;
        public static event Action OnGameOver;

        [SerializeField] private int _zombiesLeft = 4;

        private bool _isFinalWave;
        public bool IsFinalWave => _isFinalWave;

        private int _finalWaveCountdown;

        #region Unity Methods

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            _survivorsInHorde = new List<GameObject>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            //GameObject[] survivors = GameObject.FindGameObjectsWithTag("Survivors");
            //
            //foreach (GameObject survivor in survivors)
            //{
            //    Debug.Log("Adding Survivor: " +  survivor.name);
            //    //_survivorsInHorde.Enqueue(survivor);
            //}
            //
            //Debug.Log("Survivors: " + _survivorsInHorde.Count.ToString());

            _isFinalWave = false;
            _finalWaveCountdown = 30;

            OnZombieCountUpdate?.Invoke(_zombiesLeft);
            OnSurvivorCountUpdate?.Invoke(_survivorsInHorde.Count);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.V))
            {
                Debug.Log(_survivorsInHorde.Count);
            }
        }

        #endregion Unity Methods

        #region Custom Methods

        public void AddSurvivorToHorde(GameObject a_newSurvivor)
        {
            _survivorsInHorde.Add(a_newSurvivor);
            OnSurvivorCountUpdate?.Invoke(_survivorsInHorde.Count);
        }

        public void RemoveSurvivorFromHorde(GameObject a_survivor)
        {
            _survivorsInHorde.Remove(a_survivor);
            OnSurvivorCountUpdate?.Invoke(_survivorsInHorde.Count);
        }

        public void RespawnPlayer()
        {
            if (_survivorsInHorde.Count > 0)
            {
                GameObject survivor = _survivorsInHorde[0];

                if (survivor != null)
                {
                    Debug.Log(survivor.name);

                    Vector3 spawnPosition = survivor.transform.position;
                    Destroy(survivor);
                    GameObject newPlayer = Instantiate(_playerToSpawn, spawnPosition, Quaternion.identity);
                    Debug.Log("IT'S ALIIIVE!");
                    OnPlayerRespawned?.Invoke(newPlayer);

                    RemoveSurvivorFromHorde(survivor);
                }
            } else
            {
                Debug.Log("GAME OVER!");
                OnGameOver?.Invoke();
            }
        }

        public void CountdownZombies()
        {
            if (_isFinalWave)
            {
                return;
            }

            _zombiesLeft--;
            OnZombieCountUpdate?.Invoke(_zombiesLeft);

            if ( _zombiesLeft == 0)
            {
                _isFinalWave = true;
                OnFinalWaveBegins?.Invoke();
                StartCoroutine(CountdownTimerRoutine());
            }
        }

        IEnumerator CountdownTimerRoutine()
        {
            while (_isFinalWave)
            {
                OnCountdownUpdate?.Invoke(_finalWaveCountdown);
                yield return new WaitForSeconds(1);

                if (_finalWaveCountdown <= 0)
                {
                    Debug.Log("TIME HAS ENDED");
                    Time.timeScale = 0;
                    OnGameOver?.Invoke();
                }

                _finalWaveCountdown--;
                Debug.Log(_finalWaveCountdown);
            }
        }

        #endregion Custom Methods
    }
}
