using Managers;
using UnityEngine;

namespace ActTwo
{
    public class RangeEnemy : Enemy
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        private AudioSource _attackSound;

        protected override void Awake()
        {
            base.Awake();
            _attackSound = GetComponent<AudioSource>();
        }

        protected override void Attack()
        {
            var bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var bullet = bulletObject.GetComponent<DynamicBullet>();
            
            var direction = (player.position - transform.position).normalized;
            
            bullet.Initialize(bulletSpeed, damage, direction, gameObject);

            _attackSound.Play();
        }
    }
}
