using UnityEngine;

namespace ActTwo
{
    public class HomingProjectileAbility : Ability
    {
        private HomingProjectileAbilityData Data => data as HomingProjectileAbilityData;
        private void SpawnProjectile(GameObject prefab, Vector3 position, float speed, Vector3 direction)
        {
            var obj = Instantiate(prefab, position, Quaternion.identity);
            var fly = obj.GetComponent<DynamicBullet>();
            fly.Initialize(speed, Data.attackDamage, direction, transform.parent.gameObject);
        }

        private void SpawnProjectile(GameObject prefab, Vector3 position, float speed, GameObject target)
        {
            var obj = Instantiate(prefab, position, Quaternion.identity);
            var fly = obj.GetComponent<DynamicBullet>();
            fly.Initialize(speed, Data.attackDamage, target, transform.parent.gameObject);
        }

        public override void Shoot(Transform spawnPoint)
        {
            SpawnProjectile(Data.bulletPrefab, spawnPoint.position, Data.bulletSpeed, spawnPoint.forward);
        }

        public override void Special()
        {
            for (int i = 0; i < EnemiesCounter.Instance.GetEnemiesCount(); i++)
            {
                var target = EnemiesCounter.Instance.GetNearestEnemy();
                if (!target) return;
                SpawnProjectile(Data.flyPrefab, transform.parent.position, Data.flySpeed, target);
            }
        }
    }
}