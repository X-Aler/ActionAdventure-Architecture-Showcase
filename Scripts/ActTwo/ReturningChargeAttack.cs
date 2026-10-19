using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "ReturningChargeAttack", menuName = "Scriptable Objects/BossAttack/ReturningChargeAttack")]
    public class ReturningChargeAttack : BossAttack
    {
        public GameObject prefab;
        public Transform startPoint;
        public Transform endPoint;
        public float moveTime;
        public float rotateTime;
        public float impactForce;
    }
}
