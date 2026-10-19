using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "BossAttack", menuName = "Scriptable Objects/BossAttack")]
    public class BossAttack : ScriptableObject
    {
        public BossAttackType attackType;
        
        public float damage;
        public float cooldown;
    }
}
