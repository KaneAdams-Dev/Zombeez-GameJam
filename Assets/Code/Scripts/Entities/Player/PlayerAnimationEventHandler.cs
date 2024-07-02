using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZombeezGameJam.Managers;

namespace ZombeezGameJam.Entities.Player
{
    public class PlayerAnimationEventHandler : MonoBehaviour
    {
        public bool _isPlayer = false;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AttemptRespawnPlayer()
        {
            if (_isPlayer)
            {
                GameManager.instance.RespawnPlayer();
            }
        }
    }
}
