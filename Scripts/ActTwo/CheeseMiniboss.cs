using UnityEngine;

namespace ActTwo
{
    public class CheeseMiniboss : Enemy
    {
        [SerializeField] private GameObject cheese;
        [SerializeField] private GameObject cheeseRing;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float bulletDamage;

        private bool _variant;

        protected override void Attack()
        {
            if (_variant)
            {
                VariantOne();
            }
            else
            {
                VariantTwo();
            }
            _variant = !_variant;
        }

        private void VariantOne()
        {
            var cheeseObject = Instantiate(cheese, transform.position, Quaternion.identity);

            var bullet = cheeseObject.GetComponent<DynamicBullet>();
        
            bullet.Initialize(bulletSpeed, bulletDamage, player.gameObject, gameObject);
        }
    
        private void VariantTwo()
        {
            var directions = new Vector3[]
            {
                transform.forward,
                Quaternion.Euler(0, -45, 0) * transform.forward,
                Quaternion.Euler(0, 45, 0) * transform.forward
            };

            foreach (var direction in directions)
            {
                var cheeseObject = Instantiate(cheeseRing, transform.position, Quaternion.identity);

                var bullet = cheeseObject.GetComponent<DynamicBullet>();
        
                bullet.Initialize(bulletSpeed, damage, direction, gameObject);
            }
        }
    }
}
