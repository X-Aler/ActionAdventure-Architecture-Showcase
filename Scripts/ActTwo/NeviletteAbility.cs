using System.Collections;
using Controllers;
using Managers;
using Other;
using UnityEngine;

namespace ActTwo
{
    public class NeviletteAbility : Ability
    {
        private NeviletteAbilityData Data => data as NeviletteAbilityData;
        
        public override void Shoot(Transform spawnPoint)
        {
            var bulletObject = Instantiate(Data.bulletPrefab, transform.parent);
            bulletObject.transform.position = spawnPoint.position;
            var bullet = bulletObject.GetComponent<StaticBullet>();
            bullet.Initialize(Data.attackDamage, Data.attackCooldown, transform.parent.gameObject);
            AudioManager.Instance.PlayOneShot(Data.shootSound);
        }

        public override void Special()
        {
            var zoneObject = Instantiate(Data.zonePrefab, transform.parent.position, Quaternion.identity);
            var zone = zoneObject.GetComponent<NeviletteZone>();
            zone.Initialize(Data.specialDamage, Data.zoneDamageCooldown, Data.zoneSize, Data.zoneSizeSpeed, Data.zoneStayTime, transform.parent.gameObject);
            AudioManager.Instance.PlayOneShot(Data.specialSound);
        }
        
    }
}