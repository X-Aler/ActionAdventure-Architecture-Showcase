using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "ProjectileAttack", menuName = "Scriptable Objects/BossAttack/ProjectileAttack")]
    public class ProjectileAttack : BossAttack
    {
        public GameObject bulletPrefab;
        public float bulletSpeed;
    }
}