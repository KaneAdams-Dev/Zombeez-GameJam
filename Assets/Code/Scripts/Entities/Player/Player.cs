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



    public class Player : BaseEntity, IStatsApplicable
    {
        [Header("Script References")]
        [SerializeField] internal PlayerInputHandler inputScript;
        [SerializeField] internal PlayerMovementScript movementScript;
        [SerializeField] internal PlayerAnimationScript animationScript;
        [SerializeField] internal PlayerInteractorScript interactor;
        [SerializeField] internal WeaponHandler weaponHandler;

        [Space(5)]
        [SerializeField] private GameObject _playerCorpsePrefab;

        [Header("Movement Values")]
        //[SerializeField] internal float movementSpeed = 100f;
        [SerializeField] internal float jumpHeight = 10f;

        internal PlayerStates currentState;
        public WeaponTypes currentWeapon;

        public static event Action<int, int> OnHealthChange;

        #region Unity Methods

        public override void Start()
        {
            base.Start();

            currentWeapon = 0;
            UpdatePlayerState(PlayerStates.Idle);
        }

        private void OnEnable()
        {
            ColourLogger.RegisterColour(this, "purple");
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
                _playerCorpsePrefab = playerStats.CorpsePrefab;
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

        public override void UpdateHealth()
        {
            base.UpdateHealth();
            ColourLogger.Log(this, "This is a test dialogue");
            OnHealthChange?.Invoke(CurrentHealth, MaxHealth);
        }

        public override void OnDeath()
        {
            if (_playerCorpsePrefab != null)
            {
                GameObject corpse = Instantiate(_playerCorpsePrefab, transform.position, Quaternion.identity);
                corpse.transform.localScale = transform.localScale;
                corpse.GetComponentInChildren<PlayerAnimationEventHandler>()._isPlayer = true;
            }

            Destroy(gameObject);

        }

        public override void OnFall()
        {
            base.OnFall();
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

        public void PickUpWeapon(WeaponStats a_newWeapon)
        {
            currentWeapon = a_newWeapon.WeaponType;
            animationScript.UpdateAnimationState();

            weaponHandler.EquipWeapon(a_newWeapon);
        }

        #endregion Custom Methods
    }
}
