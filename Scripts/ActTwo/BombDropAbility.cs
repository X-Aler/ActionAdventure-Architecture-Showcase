using System;
using Managers;
using UnityEngine;
using PrimeTween;

namespace ActTwo
{
    public class BombDropAbility : Ability
    {
        private BombDropAbilityData Data => data as BombDropAbilityData;
        public override void Shoot(Transform spawnPoint)
        {
            var bulletObject = Instantiate(Data.bulletPrefab, spawnPoint.position, Quaternion.identity);
            var bullet = bulletObject.GetComponent<DynamicBullet>();
            bullet.Initialize(Data.bulletSpeed, Data.attackDamage, spawnPoint.forward, transform.parent.gameObject);
            AudioManager.Instance.PlayOneShot(Data.shootSound);
        }

        public override void Special()
        {
            var offset = Vector3.up * 50;
            
            var targetPosition = transform.parent.position + offset;
            
            var pigObject = Instantiate(Data.pig, targetPosition, Quaternion.identity);
            
            var bomb = pigObject.GetComponent<Bomb>();
            bomb.Initialize(Data.explosion, Data.explosionSize, Data.specialDamage, Data.explosionSpeed, Data.pigSpeed, Vector3.down, transform.parent.gameObject);
            AudioManager.Instance.PlayOneShot(Data.specialSound);
        }
    }
}