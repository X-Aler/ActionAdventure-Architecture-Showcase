using System.Collections;
using Managers;
using UnityEngine;

namespace ActTwo
{
    public class Tank : MonoBehaviour
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private Transform bulletSpawn;
        
        private float _bulletSpeed;
        private float _bulletDamage;
        private float _rotateSpeed;
        private float _attackSpeed;
        
        private GameObject _player;
        private GameObject _owner;

        private bool _canRotate = true;

        private Vector3 _direction;
        
        public void Initialize(float bulletSpeed, float bulletDamage, float rotateSpeed, float attackSpeed, GameObject player, GameObject owner)
        {
            _bulletSpeed = bulletSpeed;
            _bulletDamage = bulletDamage;
            _rotateSpeed = rotateSpeed;
            _attackSpeed = attackSpeed;
            _player = player;
            _owner = owner;
            
            StartCoroutine(Activate());
        }

        private IEnumerator Activate()
        {
            for (var i = 0; i < 4; i++)
            {
                yield return new WaitForSeconds(_attackSpeed);
                
                _canRotate = false;
                
                var bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                var bullet = bulletObject.GetComponent<DynamicBullet>();
                _direction = (_player.transform.position - transform.position).normalized;
                bullet.Initialize(_bulletSpeed, _bulletDamage, _direction, _owner);
                
                _canRotate = true;
            }
            
            Destroy(gameObject, 0.1f);
        }

        private void Update()
        {
            if (!_canRotate) return;

            _direction = (_player.transform.position - transform.position).normalized;
            
            var rotation = Quaternion.LookRotation(_direction);
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
        }
    }
}
