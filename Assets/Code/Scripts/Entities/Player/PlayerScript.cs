using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam
{
    public class PlayerScript : BaseEntity
    {
        [Header("Player Components")]
        [SerializeField] internal PlayerInputs inputScript;
        [SerializeField] internal PlayerMovement movementScript;

        internal Vector2 moveInput;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
