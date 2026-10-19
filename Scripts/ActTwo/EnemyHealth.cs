using Controllers;
using Managers;
using UnityEngine;

namespace ActTwo
{
    public class EnemyHealth : HealthController
    {
        [SerializeField] protected int moneyAmount;
        
        protected SpriteRenderer spriteRenderer;
        private Coroutine _damageCoroutine;

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            
            if (!canDamage || isDead) return;
            
            AudioManager.Instance.PlayAtObject(gameObject, AudioManager.Instance.enemyDamageSound);
            
            if (_damageCoroutine != null && this)
                StopCoroutine(_damageCoroutine);

            if (spriteRenderer)
                _damageCoroutine = StartCoroutine(DamageVisualizer.Visualize(spriteRenderer));
        }
        
        public virtual void OnDeathCommon()
        {
            MoneyManager.Instance.AddMoney(moneyAmount);
            
            if (this)
            {
                EnemiesCounter.Instance.RemoveEnemy(gameObject);
            }
        }

        protected override void Death()
        {
            if (isDead) return;
            
            OnDeathCommon();
            base.Death();
            Destroy(gameObject, 0.1f);
        }
        
    }
}