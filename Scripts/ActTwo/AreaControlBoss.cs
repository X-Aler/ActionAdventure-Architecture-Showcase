using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Playables;

namespace ActTwo
{
    public class AreaControlBoss : Boss
    {
        protected override void Shoot()
        {
            var data = GetAttackData<ProjectileAttack>(BossAttackType.Shoot);
            var bulletObject = Instantiate(data.bulletPrefab, transform.position, Quaternion.identity);
            var bullet = bulletObject.GetComponent<DynamicBullet>();
            var direction = (player.transform.position - transform.position).normalized;
            bullet.Initialize(data.bulletSpeed, data.damage, direction, gameObject);
        }

        protected override void AttackOne()
        {
            var data = GetAttackData<TankAttack>(BossAttackType.AttackOne);
            var tankObject = Instantiate(data.tankPrefab, data.tankSpawn.position, Quaternion.identity);
            var tank = tankObject.GetComponent<Tank>();
            tank.Initialize(data.tankBulletSpeed, data.damage, data.tankRotateSpeed, data.tankAttackSpeed, player, gameObject);
        }

        protected override void AttackTwo()
        {
            var data = GetAttackData<FogAttack>(BossAttackType.AttackTwo);
            var randomSpawn = data.fogSpawns[Random.Range(0, data.fogSpawns.Length)];
            var bulletObject = Instantiate(data.fogPrefab, randomSpawn.position, Quaternion.identity);
            var bullet = bulletObject.GetComponent<StaticBullet>();
            bullet.Initialize(data.damage, data.fogDamageRate, gameObject);
            bullet.DestroyBulletDuration(data.fogDuration);
        }

        protected override void PhaseAttack()
        {
            var data = GetAttackData<ReturningChargeAttack>(BossAttackType.PhaseAttack);
            var chargeObject = Instantiate(data.prefab, data.startPoint.position, data.prefab.transform.rotation);
            var chargeAttack = chargeObject.GetComponent<ReturningCharge>();
            chargeAttack.Initialize(data.damage, data.impactForce, data.moveTime, data.rotateTime, data.startPoint, data.endPoint, gameObject);
        }

        protected override void Ultimate()
        {
            StartCoroutine(BombAttack());
        }

        private IEnumerator BombAttack()
        {
            var stunTime = (float)ultimateCutscene.duration;
            
            ApplyStun(stunTime);
            player.GetComponent<HealthController>().Immunity(stunTime);
            
            ultimateCutscene.Play();
            
            AbilityManager.Instance.SetCanAttack(false);
            
            health.canDamage = false;
            
            yield return new WaitForSeconds(stunTime);
            
            var data = GetAttackData<BombAttack>(BossAttackType.Ultimate);
            
            var bomb = Instantiate(data.prefab, data.spawnPoint.position, Quaternion.identity);
            var explosion = bomb.GetComponent<Explosion>();
            explosion.Initialize(data.explosionSize, data.explosionSpeed, data.damage, gameObject);
            
            health.canDamage = true;
            AbilityManager.Instance.SetCanAttack(true);
        }
    }
}
