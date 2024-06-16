using UnityEngine;

namespace ZombeezGameJam.Entities.Player
{
    public enum PlayerStates
    {
        Idle,
        Run,
        Jump,
        Land,
        Midair,
    }

    public enum PlayerWeapons
    {
        Unarmed,
        Revolver1,
        Pistol,
        Revolver2,
        SawedoffShotgun,
        Shotgun1,
        Shotgun2,
        Uzi,
        AK,
        LMG,
    }

    public class PlayerScript : BaseEntity
    {
        [Header("Script References")]
        [SerializeField] internal PlayerInputScript inputScript;
        [SerializeField] internal PlayerMovementScript movementScript;
        [SerializeField] internal PlayerAnimationScript animationScript;
        [SerializeField] internal WeaponScript weaponScript;

        [Header("Movement Values")]
        [SerializeField] internal float movementSpeed = 100f;
        [SerializeField] internal float jumpHeight = 10f;

        internal PlayerStates currentState;
        public PlayerWeapons currentWeapon;

        private void Start()
        {
            currentWeapon = 0;
            UpdatePlayerState(PlayerStates.Idle);
        }

        internal void UpdatePlayerState(PlayerStates a_newState)
        {
            if (currentState == a_newState)
            {
                return;
            }

            if (currentState == PlayerStates.Midair && a_newState == PlayerStates.Run)
            {
                return;
            }

            currentState = a_newState;
            animationScript.UpdateAnimationState();
        }
    }
}
