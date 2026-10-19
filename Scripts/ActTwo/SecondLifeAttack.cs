using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "SecondLifeAttack", menuName = "Scriptable Objects/BossAttack/SecondLifeAttack")]
    public class SecondLifeAttack : BossAttack
    {
        public int maxStunCycles;
        public int maxStunTime;
        public int minStunTime;
        public int count;
    }
}
