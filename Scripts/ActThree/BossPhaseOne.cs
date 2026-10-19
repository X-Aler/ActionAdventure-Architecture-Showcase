using Controllers;
using UnityEngine;

namespace ActThree
{
    public class BossPhaseOne : MonoBehaviour
    {
        [SerializeField] private Qte qte;
        [SerializeField] private HealthController healthController;
        [SerializeField] private KeyCode[] keyCodes;
        [SerializeField] private float minPressTime;
        [SerializeField] private float maxPressTime;
        [SerializeField] private int damage;

        public void StartAttack()
        {
            qte.StartQte(Random.Range(minPressTime, maxPressTime), keyCodes[Random.Range(0, keyCodes.Length)], result =>
            {
                if (result) return;
                
                healthController.TakeDamage(damage);
            });
        }

        public void EndAttack()
        {
            healthController.gameObject.SetActive(true);
            healthController.Heal(100);
            healthController.gameObject.SetActive(false);
        }
    }
}
