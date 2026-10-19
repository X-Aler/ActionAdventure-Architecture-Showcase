using System.Collections;
using Controllers;
using UnityEngine;
using PrimeTween;

namespace ActTwo
{
    public class ReturningCharge : MonoBehaviour, IDamageable
    {
        private GameObject _owner;
        private Transform _startPoint;
        private Transform _endPoint;
        private float _damage;
        private float _moveTime;
        private float _rotateTime;
        private float _impactForce;

        public void Initialize(float damage, float impactForce, float moveTime, float  rotateTime, Transform startPoint, Transform endPoint, GameObject owner)
        {
            _damage = damage;
            _impactForce = impactForce;
            _moveTime = moveTime;
            _rotateTime = rotateTime;
            _startPoint = startPoint;
            _endPoint = endPoint;
            _owner = owner;
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            Tween.Position(transform, _startPoint.position, _endPoint.position, _moveTime);
            yield return new WaitForSeconds(_moveTime);
            Tween.LocalRotation(transform, new Vector3(0, 180f, 90), _rotateTime / 2f, Ease.Linear).OnComplete(() =>
                Tween.LocalRotation(transform, new Vector3(0, 360f, 90), _rotateTime / 2f));
            yield return new WaitForSeconds(_rotateTime);
            Tween.Position(transform, _endPoint.position, _startPoint.position, _moveTime);
            yield return new WaitForSeconds(_moveTime);
            Destroy(gameObject, 0.1f);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject == _owner) return;
            OnTouch(collision.gameObject);
        }
        
        private void OnTouch(GameObject other)
        {
            if (!other.TryGetComponent<PlayerController>(out var playerController)) return;
            
            Damage(other.GetComponent<HealthController>(), _damage);
            playerController.AddImpact(other.transform.forward, _impactForce);
        }

        public void Damage(HealthController healthController, float damage)
        {
            healthController.TakeDamage(damage);
        }
    }
}
