using System.Collections;
using Controllers;
using UnityEngine;

namespace ActTwo
{
    public class StaticBullet : MonoBehaviour, IDamageable
    {
        private float _damage;
        private float _attackSpeed;
        private GameObject _owner;
        private bool _canDamage = true;
        
        public void Initialize(float damage, float attackSpeed, GameObject owner)
        {
            _owner = owner;
            _damage =  damage;
            _attackSpeed = attackSpeed;
            AbilityManager.Instance.OnAttackEnd += DestroyBulletMomentum;

        }
        
        private void DestroyBulletMomentum()
        {
            if (!gameObject) return;
            Destroy(gameObject, 0.1f);
        }

        public void DestroyBulletDuration(float duration = 0.1f) => Destroy(gameObject, duration);
        
        private void OnDestroy()
        {
            AbilityManager.Instance.OnAttackEnd -= DestroyBulletMomentum;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject == _owner || !_canDamage) return;
            
            if (other.TryGetComponent<HealthController>(out var healthController))
            {
                Damage(healthController, _damage);
            }
        }

        private IEnumerator Cooldown()
        {
            _canDamage = false;
            yield return new WaitForSeconds(_attackSpeed);
            _canDamage = true;
        }
        public void Damage(HealthController healthController, float damage)
        {
            StartCoroutine(Cooldown());
            healthController.TakeDamage(damage);
        }
    }
}
