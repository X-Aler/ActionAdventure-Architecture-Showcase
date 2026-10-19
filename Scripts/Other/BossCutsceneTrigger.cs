using UnityEngine;
using UnityEngine.Playables;

namespace Other
{
    public class BossCutsceneTrigger : MonoBehaviour
    {
        [SerializeField] private PlayableDirector cutscene;
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                cutscene.Play();
            }
        }
    }
}
