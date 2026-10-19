using System;
using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;

namespace ActTwo
{
    public class LightRay : MonoBehaviour, IDamageable
    {
        private GameObject _lightScreen;
        private GameObject _owner;
        private Vector3 _size;
        private float _damage;
        private float _lightTime;
        private float _rayTime;
        private float _sizeTime;

        public void Initialize(GameObject lightScreen, float damage, float rayTime, float lightTime, float sizeTime, Vector3 size, GameObject owner)
        {
            _lightScreen = lightScreen;
            _damage = damage;
            _rayTime = rayTime;
            _lightTime = lightTime;
            _sizeTime = sizeTime;
            _size = size;
            _owner = owner;

            StartCoroutine(Activate());

        }

        private IEnumerator Activate()
        {
            Tween.Scale(transform, _size, _sizeTime);
            
            yield return new WaitForSeconds(_sizeTime + _rayTime);

            Tween.Scale(transform, Vector3.zero, _sizeTime / 2);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject == _owner) return;

            if (other.TryGetComponent<HealthController>(out var healthController))
            {
                var screen = Instantiate(_lightScreen);
                screen.SetActive(true);
                var material = _lightScreen.transform.GetChild(0).GetComponent<Image>().material;
                Tween.Custom(1, 0, _lightTime, x => {
                    var c = material.color;
                    c.a = x;
                    material.color = c;
                }).OnComplete(() => Destroy(screen, 0.1f));
                Damage(healthController, _damage);
            }
            
            Destroy(gameObject, 0.1f);
        }

        public void Damage(HealthController healthController, float damage)
        {
            healthController.TakeDamage(damage);
        }
    }
}
