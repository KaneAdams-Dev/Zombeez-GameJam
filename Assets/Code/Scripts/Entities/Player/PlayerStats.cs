using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    [CreateAssetMenu(fileName = "PlayerEntity", menuName = "Entity/PlayerEntity")]
    public class PlayerStats : BaseEntityStats
    {
        [SerializeField] private float jumpHeight;

        public float JumpHeight => jumpHeight;

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
