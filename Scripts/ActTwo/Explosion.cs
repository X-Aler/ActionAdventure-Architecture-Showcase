using System.Collections.Generic;
using Controllers;
using UnityEngine;
using PrimeTween;
namespace ActTwo
{
    public class Explosion : MonoBehaviour, IDamageable
    {
        private AudioSource _explosionSound;
        private float _damage;
        private GameObject _owner;
        
        private HashSet<GameObject> _damagedObjects = new HashSet<GameObject>();
        
        public void Initialize(float explosionSize, float explosionSpeed, float damage, GameObject owner = null)
        {
            var targetSize = Vector3.one * explosionSize;
            
            Tween.Scale(transform, Vector3.zero, targetSize, explosionSpeed).OnComplete(() => Destroy(gameObject, 0.1f));

            _damage = damage;
            _owner = owner;

            _explosionSound = GetComponent<AudioSource>();
            
            _explosionSound.Play();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _owner || _damagedObjects.Contains(other.gameObject)) return;

            if (!other.TryGetComponent<HealthController>(out var healthController)) return;
            
            Damage(healthController, _damage);
            
            _damagedObjects.Add(other.gameObject);
        }

        public void Damage(HealthController healthController, float damage)
        {
            healthController.TakeDamage(damage);
        }
    }
}
