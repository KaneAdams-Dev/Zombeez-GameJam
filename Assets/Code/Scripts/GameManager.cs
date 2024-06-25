using System;
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
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.V))
            {
                Debug.Log(_survivorsInHorde.Count);
            }
        }

        public void AddSurvivorToHorde(GameObject a_newSurvivor)
        {
            _survivorsInHorde.Add(a_newSurvivor);
        }

        public void RemoveSurvivorFromHorde(GameObject a_survivor)
        {
            _survivorsInHorde.Remove(a_survivor);
            Debug.Log(_survivorsInHorde.Count);
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
                    
                    _survivorsInHorde.RemoveAt(0);
                }
            } else
            {
                Debug.Log("GAME OVER!");
            }
        }
    }
}
