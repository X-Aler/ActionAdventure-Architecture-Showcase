using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "TankAttack", menuName = "Scriptable Objects/BossAttack/TankAttack")]
    public class TankAttack : BossAttack
    {
        public GameObject tankPrefab;
        public Transform tankSpawn;
        public float tankBulletSpeed;
        public float tankRotateSpeed;
        public float tankAttackSpeed;
    }
}
