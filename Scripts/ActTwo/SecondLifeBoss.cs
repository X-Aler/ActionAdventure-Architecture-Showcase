using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Playables;
using PrimeTween;
using Unity.VisualScripting;

namespace ActTwo
{
    public class SecondLifeBoss : Boss
    {
        private int _maxCount = 4;
        private int _minCount = 1;
        private bool _buffed;
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
            StartCoroutine(LaunchBombVolley());
        }

        private IEnumerator LaunchBombVolley()
        {
            var data = GetAttackData<BombVolleyAttack>(BossAttackType.AttackOne);
            for (var i = 0; i < Random.Range(_minCount, _maxCount); i++)
            {
                var bombObject = Instantiate(data.prefab, transform.position, Quaternion.identity);
                var bomb = bombObject.GetComponent<Bomb>();
                bomb.Initialize(data.explosionPrefab, data.explosionSize, data.damage, data.explosionTime, data.flySpeed,
                    player.gameObject, gameObject, data.bombDuration);
                yield return new WaitForSeconds(1f);
            }
        }

        protected override void AttackTwo()
        {
            StartCoroutine(RayAttack());
        }
        
        private IEnumerator RayAttack()
        {
            var data = GetAttackData<LightRayAttack>(BossAttackType.AttackTwo);
            
            for (var i = 0; i < Random.Range(_minCount, _maxCount); i++)
            {
                var spawnPosition = player.transform.position - player.transform.up;
                var warnObjcet = Instantiate(data.warnObject, spawnPosition, Quaternion.identity);
                var targetSize = new Vector3(data.raySize.x, warnObjcet.transform.localScale.y, data.raySize.z);
                Tween.Scale(warnObjcet.transform, targetSize, data.warnTime).OnComplete(() =>
                {
                    Destroy(warnObjcet);
                    
                    var spawnPos = warnObjcet.transform.position + data.prefab.transform.up * data.prefab.transform.localScale.y;
                    var rayObject = Instantiate(data.prefab, spawnPos, Quaternion.identity);
                    var ray = rayObject.GetComponent<LightRay>();
                    ray.Initialize(data.lightScreen, data.damage, data.rayTime, data.lightTime, data.raySizeTime, data.raySize, gameObject);
                });

                yield return new WaitForSeconds(1f);
            }
        }

        protected override void PhaseAttack()
        {
            var data = GetAttackData<GodRayAttack>(BossAttackType.PhaseAttack);
            var rayObject = Instantiate(data.prefab, transform.position, Quaternion.identity);
            var ray = rayObject.GetComponent<GodRay>();
            var zone = Instantiate(data.rayZone);
            ray.Initialize(data.raySize, data.raySizeTime, zone, data.damage, data.attackSpeed, data.moveSpeed, data.rayTime, gameObject);
        }

        protected override void Ultimate()
        {
            StartCoroutine(SecondLife());
        }

        private IEnumerator SecondLife()
        {
            var stunTime = (float)ultimateCutscene.duration;
            
            ApplyStun(stunTime);
            player.GetComponent<HealthController>().Immunity(stunTime);
            
            AbilityManager.Instance.SetCanAttack(false);
            
            ultimateCutscene.Play();

            health.canDamage = false;
            
            yield return new WaitForSeconds(stunTime);
            
            var data = GetAttackData<SecondLifeAttack>(BossAttackType.Ultimate);
            
            health.Heal(health.maxHealth);

            _maxCount += data.count;
            _minCount += data.count;

            maxCycles += data.maxStunCycles;
            minCycles += data.maxStunCycles;
            
            maxTime -= data.maxStunTime;
            minTime -= data.minStunTime;

            _buffed = true;
            
            health.canDamage = true;
            AbilityManager.Instance.SetCanAttack(true);
        }

        protected override void AfterInitialize()
        {
            if (!_buffed) return; 
            
            var data = GetAttackData<SecondLifeAttack>(BossAttackType.Ultimate);
            
            _maxCount -= data.count;
            _minCount -= data.count;

            maxCycles -= data.maxStunCycles;
            minCycles -= data.maxStunCycles;
            
            maxTime += data.maxStunTime;
            minTime += data.minStunTime;
            
            _buffed = false;
        }
    }
}
