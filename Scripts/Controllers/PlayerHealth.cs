using ActTwo;
using Managers;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

namespace Controllers
{
    public class PlayerHealth : HealthController
    {
        [SerializeField] private CinemachineImpulseSource impulseSource;
        [SerializeField] private Vector3 spawnPosition;
        [SerializeField] private PlayableDirector deathCutscene;
        [SerializeField] private bool isAct3;
        
        protected override void Death()
        {
            if (isAct3)
            {
                LoadingManager.Instance.Load(SceneManager.GetActiveScene().name);
                return;
            }
            
            base.Death();
            Immunity(3);
            
            AudioManager.Instance.StopAllLoops();
            TeleportManager.Instance.Teleport(spawnPosition);
            
            if (EnemiesCounter.Instance)
            {
                EnemiesCounter.Instance.ResetDanger();
            }
            
            if (MoneyManager.Instance)
            {
                MoneyManager.Instance.NullMoney();
            }

            foreach (var dir in FindObjectsByType<PlayableDirector>(FindObjectsInactive.Exclude, FindObjectsSortMode.None))
            {
                if (dir.state == PlayState.Playing)
                {
                    dir.time = dir.duration; 
                    dir.Evaluate();
                    dir.Stop();
                }
            }
            
            deathCutscene.Play();
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.playerDeathSound);
            
            Heal(maxHealth);
            
            isDead = false;
        }
        

        public override void TakeDamage(float amount)
        {
            base.TakeDamage(amount);
            
            if (!canDamage || isDead) return;
            
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.playerDamageSound);
            impulseSource.GenerateImpulseWithForce(20);
        }
    }
}
