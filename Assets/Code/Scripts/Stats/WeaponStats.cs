using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace ZombeezGameJam.Stats
{
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Weapons")]
    public class WeaponStats : ScriptableObject
    {
        [SerializeField] WeaponTypes _weaponType;

        [SerializeField] Sprite _pickupSprite;
        public Sprite PickupSprite => _pickupSprite;

        public WeaponTypes WeaponType => _weaponType;

        [Header("Prefabs")]
        [SerializeField] private GameObject _MuzzleFlash;
        public GameObject MuzzleFlash => _MuzzleFlash;

        [SerializeField] private GameObject _bulletPrefab;

        public GameObject BulletPrefab => _bulletPrefab;

        [Header("Weapon Stats")]
        [SerializeField] int _damage;
        public int Damage => _damage;

        [SerializeField][Range(0.5f, 2)] private float _bulletDropoff;
        public float BulletDropoff => _bulletDropoff;

        [Space(5)]
        [SerializeField] private float _fireRate;
        public float FireRate => _fireRate;

        [SerializeField] private bool _isAutomatic;
        public bool IsAutomatic => _isAutomatic;

        [Header("Audio")]
        [SerializeField] private AudioClip _gunshotAudio;
        public AudioClip GunShotAudio => _gunshotAudio;

        [Space(5)]
        [SerializeField][Range(0, 3)] private float _audioPitch;
        public float AudioPitch => _audioPitch;

        [SerializeField] private Vector2 _pitchRange;
        public Vector2 PitchRange => _pitchRange;
    }
}
