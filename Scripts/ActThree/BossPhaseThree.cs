using UnityEngine;

namespace ActThree
{
    public class BossPhaseThree : MonoBehaviour
    {
        [SerializeField] private float maxSpawnCooldown;
        [SerializeField] private float minSpawnCooldown;
        
        [SerializeField] private float maxClickTime;
        [SerializeField] private float minClickTime;

        public void StartAttack()
        {
            Osu.Instance.StartGame(maxSpawnCooldown, minSpawnCooldown, maxClickTime, minClickTime);
        }

        public void StopAttack()
        {
            Osu.Instance.StopGame();
        }
    }
}
