using UnityEngine;

namespace ActTwo
{
    public enum ShootType
    {
        Hold,
        Single
    }
    public abstract class AbilityData : ScriptableObject
    {
        public Abilities ability;
        public GameObject handPrefab;

        public ShootType shootType;
        
        public AudioClip shootSound;
        public AudioClip specialSound;
        
        public float attackCooldown;
        public float specialCooldown;
        
        public float attackDamage;
        public float specialDamage;
    }
}