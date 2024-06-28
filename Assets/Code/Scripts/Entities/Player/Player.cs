using System;
using UnityEngine;
using ZombeezGameJam.Interfaces;
using ZombeezGameJam.Stats;

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
        PolicePistol,
        Glock,
        Magnum,
        SawnOff,
        PumpAction,
        AssualtShotgun,
        Uzi,
        AK47,
        M60,
    }

    public class Player : BaseEntity, IStatsApplicable
    {
        [Header("Script References")]
        [SerializeField] internal PlayerInputHandler inputScript;
        [SerializeField] internal PlayerMovementScript movementScript;
        [SerializeField] internal PlayerAnimationScript animationScript;
        [SerializeField] internal PlayerInteractorScript interactor;
        [SerializeField] internal WeaponScript weaponScript;

        [Header("Movement Values")]
        //[SerializeField] internal float movementSpeed = 100f;
        [SerializeField] internal float jumpHeight = 10f;

        internal PlayerStates currentState;
        public PlayerWeapons currentWeapon;

        public static event Action<int, int> OnHealthChange;

        #region Unity Methods

        public override void Start()
        {
            base.Start();

            currentWeapon = 0;
            UpdatePlayerState(PlayerStates.Idle);
        }

        #endregion Unity Methods

        #region Custom Methods

        public override void ApplyEntityStats()
        {
            base.ApplyEntityStats();
            
            animationScript._animator.runtimeAnimatorController = _stats.Controller;

            if (_stats is PlayerStats playerStats)
            {
                jumpHeight = playerStats.JumpHeight;
            }
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

        public void EquipWeapon(PlayerWeapons a_newWeapon)
        {
            currentWeapon = a_newWeapon;
            animationScript.UpdateAnimationState();
        }

        public override void UpdateHealth()
        {
            base.UpdateHealth();
            OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
        }

        public override void OnDeath()
        {
            base.OnDeath();
            GameManager.instance.RespawnPlayer();
        }

        public override void OnFall()
        {
            base.OnFall();
            GameManager.instance.RespawnPlayer();
        }

        public void SetCurrentStatSO(BaseEntityStats a_newStats)
        {
            _stats = a_newStats;
            ApplyEntityStats();
        }

        public BaseEntityStats GetEntityStats()
        {
            return _stats;
        }

        public int GetCurrentHealth()
        {
            return CurrentHealth;
        }

        public void SetInitialHealth(int a_startHealth)
        {
            CurrentHealth = a_startHealth;
            OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
        }

        #endregion Custom Methods
    }
}
