using Controllers;
using Managers;
using UnityEngine;

namespace Other
{
    public class LavaDamager : MonoBehaviour
    {
        [SerializeField] private Transform spawnTarget;
        [SerializeField] private float damage = 25;
        [SerializeField] private HealthController playerHealth;

        private bool _damageCd;
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") && _damageCd) return;

            _damageCd = true;
            
            playerHealth.TakeDamage(damage);

            TeleportManager.Instance.Teleport(spawnTarget);
            
            _damageCd = false;
        }
    }
}