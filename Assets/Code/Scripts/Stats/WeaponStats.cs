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

        [SerializeField] private GameObject _MuzzleFlash;
        public GameObject MuzzleFlash => _MuzzleFlash;

        [SerializeField] private GameObject _bulletPrefab;

        public GameObject BulletPrefab => _bulletPrefab;

        [SerializeField] int _damage;
        public int Damage => _damage;

        [SerializeField][Range(0.5f, 2)] private float _bulletDropoff;
        public float BulletDropoff => _bulletDropoff;

        [SerializeField] private float _fireRate;
        public float FireRate => _fireRate;

        [SerializeField] private bool _isAutomatic;
        public bool IsAutomatic => _isAutomatic;

        [SerializeField] private AudioClip _gunshotAudio;
        public AudioClip GunShotAudio => _gunshotAudio;
    }
}
