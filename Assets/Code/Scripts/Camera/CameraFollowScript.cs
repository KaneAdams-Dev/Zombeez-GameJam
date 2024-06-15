using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class CameraFollowScript : MonoBehaviour
    {
        [SerializeField] private Transform objectToFollow;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        // LateUpdate is called every frame, if the Behaviour is enabled
        private void LateUpdate()
        {
            if (objectToFollow != null)
            {
                transform.position = new Vector3(objectToFollow.position.x, objectToFollow.position.y + 0.25f, transform.position.z);
            }
        }


    }
}
