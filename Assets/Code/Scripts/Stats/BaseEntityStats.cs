using UnityEngine;

namespace ZombeezGameJam.Stats
{
    [CreateAssetMenu(fileName ="BaseEntity", menuName = "Entity/BaseEntity")]
    public class BaseEntityStats : ScriptableObject
    {
        [Header("Health")]
        [SerializeField][Min(10)] private int _maxHealth;
        [SerializeField][Min(0)] private int _defense;
        [Space(5)]
        [SerializeField] private GameObject corpsePrefab;
        
        public int MaxHealth => _maxHealth;
        public int Defense => _defense;
        public GameObject CorpsePrefab => corpsePrefab;


        [Header("Movement")]
        [SerializeField] private float _movementSpeed;
        public float MovementSpeed => _movementSpeed;


        [Header("Animations")]
        [SerializeField] private RuntimeAnimatorController _animatorController;

        public RuntimeAnimatorController Controller => _animatorController;
    }
}
