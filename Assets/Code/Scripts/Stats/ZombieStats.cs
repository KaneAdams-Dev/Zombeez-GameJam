using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZombeezGameJam.Stats
{
    [CreateAssetMenu(fileName = "ZombieEntity", menuName = "Entity/ZombieEntity")]
    public class ZombieStats : BaseEntityStats
    {
        [Header("AI Ranges")]
        [SerializeField] private float _chaseRange;
        [SerializeField] private float _chaseBuffer;
        [SerializeField] private float _attackRange;

        public float ChaseRange => _chaseRange;
        public float ChaseBuffer => _chaseBuffer;
        public float AttackRange => _attackRange;


        [Header("Zombie Type")]
        [SerializeField] private int _attackStrength;
        
        [Space(10)]
        [SerializeField] private bool _isShuffler;
        [SerializeField] private bool _hasSecondAttack;

        [SerializeField] private float _secondaryAttackRange;

        public int AttackStrength => _attackStrength; 

        public bool IsShuffler => _isShuffler;
        public bool HasSecondAttack => _hasSecondAttack;

        public float SecondaryAttackRange => _secondaryAttackRange;


        [Header("Audio")]
        [SerializeField] private AudioClip[] _movementAudio;
        [SerializeField] private AudioClip _attackAudio;
        [SerializeField] private AudioClip _deathAudio;

        public AudioClip[] MovementAudio => _movementAudio;
        public AudioClip AttackAudio => _attackAudio;

        public AudioClip DeathAudio => _deathAudio;
    }
}
