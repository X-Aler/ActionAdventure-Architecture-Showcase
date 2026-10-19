using UnityEngine;
using PrimeTween;

namespace Other
{
    public class BridgeTrigger : MonoBehaviour
    {
        [SerializeField] private GameObject bridge;
        
        private bool _triggered;
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                if (_triggered) return;
                Tween.Scale(bridge.transform, Vector3.zero, new Vector3(4,1,29.5f), 1f);
                _triggered = true;
            }
        }
    }
}
