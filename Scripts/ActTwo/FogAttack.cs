using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "FogAttack", menuName = "Scriptable Objects/BossAttack/FogAttack")]
    public class FogAttack : BossAttack
    {
        public Transform[] fogSpawns;
        public GameObject fogPrefab;
        public float fogDamageRate;
        public float fogDuration;
    }
}
