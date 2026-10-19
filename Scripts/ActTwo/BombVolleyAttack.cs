using UnityEngine;
using UnityEngine.Serialization;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "BombVolleyAttack", menuName = "Scriptable Objects/BossAttack/BombVolleyAttack")]
    public class BombVolleyAttack : BossAttack
    {
        public GameObject prefab;
        public GameObject explosionPrefab;
        public float explosionSize;
        public float explosionTime;
        public float flySpeed;
        public float bombDuration;
    }
}
