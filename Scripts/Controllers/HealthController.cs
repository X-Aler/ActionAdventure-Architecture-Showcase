using System;
using System.Collections;
using ActTwo;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class HealthController : MonoBehaviour
    {
        public float maxHealth = 100f;
        public float currentHealth;
        
        public event Action<float, float> OnHealthChanged;
        public event Action OnDeath;

        public bool canDamage;
        
        public bool isDead { get; protected set; }
        
        private Coroutine _immunityCoroutine;
        private Coroutine _regenCoroutine;
        private Coroutine _regenTimerCoroutine;

        protected virtual void Awake()
        {
            currentHealth = maxHealth;
            canDamage = true;
        }

        private void OnEnable()
        {
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
            RestartRegen();

            if (_immunityCoroutine != null)
            {
                StopCoroutine(_immunityCoroutine);
                canDamage = true;
            }
        }

        public virtual void TakeDamage(float damage)
        {
            if (!canDamage || isDead) return;

            if (currentHealth >= damage)
            {
                currentHealth -= damage;
            
                OnHealthChanged?.Invoke(currentHealth, maxHealth);
            
                RestartRegen();
            }
            else
            {
                Death();
            }
        }

        public virtual void Heal(float amount)
        {
            currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
            
            OnHealthChanged?.Invoke(currentHealth, maxHealth);
        }

        public bool GetImmunityStatus()
        {
            return canDamage;
        }

        public void Immunity(float time)
        {
            if (_immunityCoroutine != null)
            {
                StopCoroutine(_immunityCoroutine);
            }
            
            canDamage = false;
            
            _immunityCoroutine = StartCoroutine(ImmunityCoroutine(time));
        }

        private IEnumerator ImmunityCoroutine(float time)
        {
            yield return new WaitForSeconds(time);
            
            canDamage = true;
        }
        

        private void RestartRegen()
        {
            if (_regenTimerCoroutine != null)
            {
                StopCoroutine(_regenTimerCoroutine);
            }

            if (_regenCoroutine != null)
            {
                StopCoroutine(_regenCoroutine);
            }
            _regenTimerCoroutine = StartCoroutine(RegenTimer());
        }

        private IEnumerator RegenTimer()
        {
            yield return new WaitForSeconds(10f);
            
            if (_regenCoroutine != null)
            {
                StopCoroutine(_regenCoroutine);
            }
            
            _regenCoroutine = StartCoroutine(StartRegen());
        }

        private IEnumerator StartRegen()
        {
            while (currentHealth < maxHealth)
            {
                yield return new WaitForSeconds(3f);
                Heal(1);
            }
        }
        
        protected virtual void Death()
        {
            isDead = true;
            OnDeath?.Invoke();
        }
    }
}
