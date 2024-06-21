using UnityEngine;

namespace ZombeezGameJam.Entities
{
    [CreateAssetMenu(fileName ="BaseEntity", menuName = "Entity/BaseEntity")]
    public class BaseEntityStats : ScriptableObject
    {
        [SerializeField] private int _maxHealth;
        [SerializeField] private int _defense;
        [SerializeField] private RuntimeAnimatorController _animatorController;

        public RuntimeAnimatorController Controller => _animatorController;
    }
}
