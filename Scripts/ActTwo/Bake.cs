using System;
using System.Collections;
using Controllers;
using UnityEngine;

namespace ActTwo
{
    public class Bake : MonoBehaviour
    {
        [SerializeField] private GameObject door;

        private float _time;
        private float _damage;
        private float _strength;
        private float _distance;
        private float _speed;

        private GameObject _owner;
        private Attraction _attraction;
        private Vector3 _moveDirection;
        private bool _shouldMove;
        
        private Coroutine _despawnCoroutine;
        private AudioSource _flameSound;

        public void Initialize(float time, float damage, float strength, float distance, float speed, GameObject owner)
        {
            _time = time;
            _damage = damage;
            _strength = strength;
            _distance = distance;
            _speed = speed;
            _owner = owner;
            
            _attraction = GetComponent<Attraction>();
            _attraction.Initialize(_strength, _distance, _owner);
            _attraction.AttractedEnd += Damage;

            _moveDirection = Vector3.zero;
            _shouldMove = false;

            _flameSound = GetComponent<AudioSource>();
            
            StartCoroutine(Timer());
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_time);

            if (_despawnCoroutine != null) yield break;

            _despawnCoroutine = StartCoroutine(Despawn());
        }

        private IEnumerator Despawn()
        {
            if (door.TryGetComponent(out Animator animator))
            {
                animator.SetTrigger("Open");
                door.GetComponent<AudioSource>().Play();
            }

            yield return new WaitForSeconds(2f);
            
            _moveDirection = -transform.up;
            _shouldMove = true;
            
            _flameSound.Play();

            yield return new WaitForSeconds(3f);
            Destroy(gameObject);
        }

        private void Update()
        {
            if (_shouldMove)
                transform.Translate(_moveDirection * _speed * Time.deltaTime, Space.World);
        }

        private void Damage(GameObject target)
        {
            StartCoroutine(Despawn());
            if (!target.TryGetComponent<HealthController>(out var healthController)) return;
            
            healthController.TakeDamage(_damage);
            if (target.TryGetComponent<Enemy>(out var enemy))
                enemy.stunned = false;
        }

        private void OnDestroy()
        {
            if (!_attraction)  return;
            _attraction.AttractedEnd -= Damage; 
        }
    }
}
