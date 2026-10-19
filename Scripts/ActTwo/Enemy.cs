using System.Collections;
using Controllers;
using UnityEngine;

namespace ActTwo
{
    public abstract class Enemy : MonoBehaviour
    {
        public float speed;
        public float damage;

        public float attackRange = 2f;
        public float detectRange = 10f;
        public float attackCooldown = 1.5f;
        public float pushForce = 1f;
        
        public bool stunned;
        
        protected Transform player;
        protected HealthController healthController;
        protected PlayerController playerController;
        
        private bool _playerDetected;
        private bool _isCooldown;
        
        private Vector3 _spawnPosition;
        
        protected virtual void Awake()
        {
            player = FindAnyObjectByType<PlayerController>().transform;
            _spawnPosition = transform.position;
        }

        protected virtual void Update()
        {
            if (stunned) return;
            DetectPlayer();

            if (!_playerDetected)
            {
                if (Vector3.Distance(transform.position, _spawnPosition) > 5f)
                {
                    transform.position = _spawnPosition;
                }
                return;
            }
            
            FollowPlayer();

            if (!CanAttack()) return;
            Attack();
            StartCoroutine(Cooldown());
        }

        private void DetectPlayer()
        {
            if (!player) return;
            
            var distance = Vector3.Distance(transform.position, player.position);
            _playerDetected = distance <= detectRange;
        }
        
        protected virtual void FollowPlayer()
        {
            var distance = Vector3.Distance(transform.position, player.position);

            if (distance > attackRange)
            {
                var direction = (player.position - transform.position).normalized;
                transform.position += direction * speed * Time.deltaTime;
            }

            transform.LookAt(player);
        }

        private bool CanAttack()
        {
            return Vector3.Distance(transform.position, player.position) <= attackRange && !_isCooldown;
        }

        private IEnumerator Cooldown()
        {
            _isCooldown = true;
            yield return new WaitForSeconds(attackCooldown);
            _isCooldown = false;
        }
        
        protected abstract void Attack();

    }
}
