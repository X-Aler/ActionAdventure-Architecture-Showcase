using System.Collections;
using System.Collections.Generic;
using Controllers;
using UnityEngine;
using PrimeTween;
using Unity.VisualScripting;

namespace ActTwo
{
    public class ExplosionBullet : MonoBehaviour
    {
        private float _damage;
        private float _scaleTime;
        private float _endScale;
        
        private GameObject _owner;
        
        private readonly List<HealthController> _healthControllers = new List<HealthController>();

        public void Initialize(float damage, float scaleTime, float endScale, GameObject owner)
        {
            _damage = damage;
            _scaleTime = scaleTime;
            _endScale = endScale;
            _owner = owner;

            StartCoroutine(ExplodeCooldown());
        }

        private IEnumerator ExplodeCooldown()
        {
            Tween.Scale(transform, 0, _endScale, _scaleTime);
            
            yield return new WaitForSeconds(_scaleTime);
            
            Explode();
        }

        private void Explode()
        {
            if (_healthControllers.Count > 0)
            {
                foreach (var healthController in _healthControllers)
                {
                    healthController.TakeDamage(_damage);
                }
            }
            
            Destroy(gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _owner) return;

            if (TryGetComponent<HealthController>(out var healthController))
            {
                _healthControllers.Add(healthController);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject == _owner) return;

            if (!TryGetComponent<HealthController>(out var healthController) || !_healthControllers.Contains(healthController)) return;
            
            _healthControllers.Remove(healthController);
        }
        
    }
}
