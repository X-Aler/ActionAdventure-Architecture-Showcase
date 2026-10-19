using System.Collections;
using PrimeTween;
using UnityEngine;

namespace ActTwo
{
    public class Bomb : MonoBehaviour
    {
        private GameObject _explosionPrefab;
        private GameObject _owner;
        private float _explosionSize;
        private float _explosionDamage;
        private float _explosionSpeed;
        private float _flySpeed;
        private float _duration;
        private Vector3 _targetDirection;
        private GameObject _targetObject;

        private bool _activated = false;

        private void InitCommon(GameObject prefab, float size, float damage, float explosionSpeed, float flySpeed, float duration, GameObject owner)
        {
            _explosionPrefab = prefab;
            _explosionSize = size;
            _explosionDamage = damage;
            _explosionSpeed = explosionSpeed;
            _flySpeed = flySpeed;
            _duration = duration;
            _owner = owner;

            if (_duration > 0)
            {
                StartCoroutine(Explode());
            }
        }

        public void Initialize(GameObject prefab, float size, float damage, float explosionSpeed, float flySpeed, Vector3 direction, GameObject owner, float duration = 0f)
        {
            InitCommon(prefab, size, damage, explosionSpeed, flySpeed, duration, owner);
            _targetDirection = direction;
            _targetObject = null;
        }

        public void Initialize(GameObject prefab, float size, float damage, float explosionSpeed, float flySpeed, GameObject directionObject, GameObject owner , float duration = 0f)
        {
            InitCommon(prefab, size, damage, explosionSpeed, flySpeed, duration, owner);
            _targetObject = directionObject;
            _targetDirection = Vector3.zero;
        }

        private IEnumerator Explode()
        {
            yield return new WaitForSeconds(_duration);
            
            Explosion();
        }

        private void Explosion()
        {
            if (_activated) return;
            _activated = true;
            
            var explosionObject = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            
            var explosion = explosionObject.GetComponent<Explosion>();
            explosion.Initialize(_explosionSize, _explosionSpeed, _explosionDamage, _owner);
            
            Tween.Scale(transform, transform.localScale, Vector3.zero, _explosionSpeed).OnComplete(() => Destroy(gameObject, 0.1f));
        }
 
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _owner) return;
            
            Explosion();
        }

        private void Update()
        {
            if (_activated) return;
            
            if (_targetObject)
            {
                _targetDirection = (_targetObject.transform.position - transform.position).normalized;
                transform.forward = _targetDirection;
            }
            
            if (gameObject && _targetDirection != Vector3.zero)
            {
                transform.Translate(_targetDirection * _flySpeed * Time.deltaTime, Space.World);
            }
        }
    }
}