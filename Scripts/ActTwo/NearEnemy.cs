using Controllers;
using Managers;
using UnityEngine;

namespace ActTwo
{
    public class NearEnemy : Enemy
    {
        private AudioSource _attackSound;
        
        protected override void Awake()
        {
            base.Awake();
            _attackSound = GetComponent<AudioSource>();
        }

        protected override void Attack()
        {
            if (!healthController)
            {
                healthController = player.GetComponent<HealthController>();
            }
            
            healthController.TakeDamage(damage);

            if (!playerController)
            {
                playerController = player.GetComponent<PlayerController>();
            }
            
            playerController.AddImpact(transform.forward, pushForce);
            _attackSound.Play();
        }
    }
}
