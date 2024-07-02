using UnityEngine;
using ZombeezGameJam.Managers;

namespace ZombeezGameJam.Camera
{
    public class CameraFollowScript : MonoBehaviour
    {
        public static CameraFollowScript instance;
        
        private Transform _objectToFollow;

        #region Unity Methods

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
                return;
            }

            instance = this;
        }

        private void OnEnable()
        {
            GameManager.OnPlayerRespawned += SetObjectToFollow;
        }

        private void OnDisable()
        {
            GameManager.OnPlayerRespawned -= SetObjectToFollow;
        }

        private void Start()
        {
            _objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void LateUpdate()
        {
            if (_objectToFollow == null)
            {
                return;
            }

            transform.position = new Vector3(_objectToFollow.position.x, Mathf.Clamp(_objectToFollow.position.y + 0.25f, 0f, 100f), transform.position.z);
        }

        #endregion Unity Methods

        #region Custom Methods

        //public void FindPlayer()
        //{
        //    _objectToFollow = GameObject.FindGameObjectWithTag("Player").transform;
        //    Debug.Log("Found: " + _objectToFollow.name);
        //}

        private void SetObjectToFollow(GameObject a_newPlayer)
        {
            _objectToFollow = a_newPlayer.transform;
        }

        #endregion Custom Methods
    }
}
