using UnityEngine;

namespace ActOne
{
    public class DamageOverTime : MonoBehaviour
    {
        [SerializeField] private DamageOverTimeController damageOverTimeController;

        private void Awake()
        {
            if (!damageOverTimeController)
            {
                damageOverTimeController = FindAnyObjectByType<DamageOverTimeController>();
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            damageOverTimeController.Enter();
        }
        
    }
}
