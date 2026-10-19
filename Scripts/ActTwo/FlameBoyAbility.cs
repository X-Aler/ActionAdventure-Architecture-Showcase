using System.Collections;
using Controllers;
using UnityEngine;

namespace ActTwo
{
    public class FlameBoyAbility : Ability
    {
        private FlameBoyAbilityData Data => data as FlameBoyAbilityData;
        
        public override void Shoot(Transform spawnPoint)
        {
            var bulletObject = Instantiate(Data.bulletPrefab, transform.parent);
            bulletObject.transform.position = spawnPoint.position;
            var bullet = bulletObject.GetComponent<StaticBullet>();
            bullet.Initialize(Data.attackDamage, Data.attackCooldown, transform.parent.gameObject);
        }

        public override void Special()
        {
            var spawnPosition = transform.parent.position + transform.parent.forward * 15;
            var maxRayDistance = 10f;

            if (!Physics.Raycast(spawnPosition + Vector3.up * 5f, Vector3.down, out var hit, maxRayDistance,
                    Data.bakeLayerMask)) return;
            
            var bakeObject = Instantiate(Data.bakePrefab, hit.point, Quaternion.identity);
            
            var lookDirection = transform.parent.position - bakeObject.transform.position;
            lookDirection.y = 0;

            bakeObject.transform.forward = lookDirection;

            var bake = bakeObject.GetComponent<Bake>();
            bake.Initialize(Data.attractionTime, Data.specialDamage, Data.attractionStrength, Data.attractionDistance, Data.bakeSpeed, transform.parent.gameObject);
        }
        
    }
}