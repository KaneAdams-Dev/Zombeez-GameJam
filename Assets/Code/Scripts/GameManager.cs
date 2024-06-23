using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance;

        private Queue<GameObject> _survivorsInHorde;
        [SerializeField] private GameObject _playerToSpawn;         // TEMP 

        public static event Action<GameObject> OnPlayerRespawned;

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;

            _survivorsInHorde = new Queue<GameObject>();
        }

        // Start is called before the first frame update
        private void Start()
        {
            GameObject[] survivors = GameObject.FindGameObjectsWithTag("Survivors");

            foreach (GameObject survivor in survivors)
            {
                Debug.Log("Adding Survivor: " +  survivor.name);
                _survivorsInHorde.Enqueue(survivor);
            }

            Debug.Log("Survivors: " + _survivorsInHorde.Count.ToString());
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RespawnPlayer()
        {
            if (_survivorsInHorde.Count > 0)
            {
                GameObject survivor = _survivorsInHorde.Dequeue();

                if (survivor != null)
                {
                    Debug.Log(survivor.name);

                    Vector3 spawnPosition = survivor.transform.position;
                    Destroy(survivor);
                    GameObject newPlayer = Instantiate(_playerToSpawn, spawnPosition, Quaternion.identity);
                    Debug.Log("IT'S ALIIIVE!");

                    OnPlayerRespawned?.Invoke(newPlayer);
                }
            } else
            {
                Debug.Log("GAME OVER!");
            }
        }
    }
}
