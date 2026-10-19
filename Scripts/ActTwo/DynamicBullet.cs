using Controllers;
using UnityEngine;

namespace ActTwo
{
    public class DynamicBullet : MonoBehaviour, IDamageable
    {
        private float _flySpeed;
        private float _damage;
        private Vector3 _flyDirection;
        private GameObject _flyDirectionObject;
        private GameObject _owner;
        
        public void Initialize(float speed, float damage, Vector3 direction, GameObject owner)
        {
            _flySpeed = speed;
            _flyDirection = direction;
            transform.up = _flyDirection;
            _owner = owner;
            _damage =  damage;
            Destroy(gameObject, 5f);
        }
        
        public void Initialize(float speed, float damage, GameObject directionObject, GameObject owner)
        {
            _flySpeed = speed;
            _flyDirectionObject = directionObject;
            _owner = owner;
            _damage =  damage;
            Destroy(gameObject, 5f);
        }

        private void Update()
        {
            if (_flyDirectionObject)
            {
                _flyDirection = (_flyDirectionObject.transform.position - transform.position).normalized;
                transform.up = _flyDirection;
            }
            
            if (_flyDirection != Vector3.zero)
            {
                transform.Translate(_flyDirection * _flySpeed * Time.deltaTime, Space.World);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _owner) return;
            
            if (other.TryGetComponent<HealthController>(out var healthController))
            {
                Damage(healthController, _damage);
            }
            else
            {
                return;
            }

            if (gameObject)
            {
                Destroy(gameObject, 0.1f);
            }
        }

        public void Damage(HealthController healthController, float damage)
        {
            healthController.TakeDamage(damage);
        }
    }
}
