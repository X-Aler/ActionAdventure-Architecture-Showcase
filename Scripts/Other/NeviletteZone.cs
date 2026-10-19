using System;
using System.Collections;
using System.Collections.Generic;
using ActTwo;
using Controllers;
using UnityEngine;
using PrimeTween;

namespace Other
{
    public class NeviletteZone : MonoBehaviour, IDamageable
    {
        private GameObject _owner;
        private float _damage;
        private float _damageCooldown;
        private float _size;
        private float _sizeSpeed;
        private float _stayTime;
        
        private List<HealthController> _targets = new List<HealthController>();

        public void Initialize(float damage, float damageCooldown, float size, float speed, float stayTime, GameObject owner)
        {
            _damage = damage;
            _damageCooldown = damageCooldown;
            _size = size;
            _sizeSpeed = speed;
            _stayTime = stayTime;
            _owner = owner;
            
            StartCoroutine(Arrive());
            StartCoroutine(DamageOverTime());
        }

        private IEnumerator Arrive()
        {
            Tween.Scale(transform, Vector3.zero, new Vector3(_size, 0.1f, _size), _sizeSpeed);
            
            yield return new WaitForSeconds(_sizeSpeed);
            yield return new WaitForSeconds(_stayTime);
            
            Tween.Scale(transform, transform.localScale, Vector3.zero, _sizeSpeed);
            
            yield return new WaitForSeconds(_sizeSpeed);
            
            Destroy(gameObject, 0.1f);
        }

        private IEnumerator DamageOverTime()
        {
            while (gameObject)
            {
                if (_targets.Count > 0)
                {
                    foreach (var target in _targets)
                        Damage(target, _damage);
                }

                yield return new WaitForSeconds(_damageCooldown);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            HandleTrigger(other, true);
        }

        private void OnTriggerExit(Collider other)
        {
            HandleTrigger(other, false);
        }

        private void HandleTrigger(Collider other, bool isEnter)
        {
            if (other.gameObject == _owner) return;

            if (other.TryGetComponent(out Enemy enemy))
            {
                enemy.stunned = isEnter;
            }
            
            if (other.TryGetComponent(out Boss boss))
            {
                boss.ApplyStun(_stayTime);
            }

            if (!other.TryGetComponent(out HealthController healthController)) return;
            
            if (isEnter)
                _targets.Add(healthController);
            else
                _targets.Remove(healthController);
        }

        public void Damage(HealthController healthController, float damage)
        {
            healthController.TakeDamage(damage);
        }
    }
}
