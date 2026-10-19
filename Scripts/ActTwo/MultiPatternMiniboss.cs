using UnityEngine;

namespace ActTwo
{
    public class MultiPatternMiniboss : Enemy
    {
        [SerializeField] private GameObject bullet;
        [SerializeField] private GameObject fly;
        [SerializeField] private GameObject explosionBullet;
        
        [SerializeField] private float bulletSpeed;
        
        [SerializeField] private float flyDamage;
        [SerializeField] private float flySpeed;
        
        [SerializeField] private float explosionDamage;
        [SerializeField] private float explosionScale;
        [SerializeField] private float explosionTime;

        private int _variant;

        private AudioSource _shootSound;

        protected override void Awake()
        {
            base.Awake();
            _shootSound = GetComponent<AudioSource>();
        }

        protected override void Attack()
        {
            switch (_variant)
            {
                case 0:
                    VariantOne();
                    break;
                case 1:
                    VariantTwo();
                    break;
                case 2:
                    VariantThree();
                    break;
            }

            _variant = Random.Range(0, 3);
        }

        private void VariantOne()
        {
            var flyDirection = (player.position - transform.position).normalized;
            
            SpawnBullet(bullet, flyDirection);
            
            _shootSound.Play();
        }
    
        private void VariantTwo()
        {
            SpawnBullet(fly, player.gameObject);
        }
        
        private void VariantThree()
        {
            var spawnPosition = player.transform.position - player.transform.up;
            
            var explodeObject = Instantiate(explosionBullet, spawnPosition, Quaternion.identity);

            var explode = explodeObject.GetComponent<ExplosionBullet>();
            
            explode.Initialize(explosionDamage, explosionTime, explosionScale, gameObject);
        }

        private void SpawnBullet(GameObject bulletPrefab, object directionSource)
        {
            var bulletObject = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var dynamicBullet = bulletObject.GetComponent<DynamicBullet>();

            if (directionSource is Vector3 dir)
            {
                dynamicBullet.Initialize(bulletSpeed, damage, dir, gameObject);
            }
            else if (directionSource is GameObject obj)
            {
                dynamicBullet.Initialize(flySpeed, flyDamage, obj, gameObject);
            }
        }
    }
}
