using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "GodRayAttack", menuName = "Scriptable Objects/BossAttack/GodRayAttack")]
    public class GodRayAttack : BossAttack
    {
        public GameObject prefab;
        public GameObject rayZone;
        public float attackSpeed;
        public float moveSpeed;
        public float rayTime;
        public float raySizeTime;
        public Vector3 raySize;
    }
}
