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
        [SerializeField] private bool _isShuffler;
        [SerializeField] private bool _hasSecondAttack;

        public bool IsShuffler => _isShuffler;
        public bool HasSecondAttack => _hasSecondAttack;


        [Header("Audio")]
        [SerializeField] private AudioClip[] _movementAudio;
        [SerializeField] private AudioClip _attackAudio;

        public AudioClip[] MovementAudio => _movementAudio;
        public AudioClip AttackAudio => _attackAudio;
    }
}
