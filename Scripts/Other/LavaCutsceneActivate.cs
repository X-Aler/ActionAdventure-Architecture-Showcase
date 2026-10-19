using System;
using Controllers;
using UnityEngine;
using UnityEngine.Playables;

namespace Other
{
    public class LavaCutsceneActivate : MonoBehaviour
    {
        [SerializeField] private TurnableController turnableController;
        [SerializeField] private PlayableDirector cutscene;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            if (turnableController.cutsceneActive)
            {
                cutscene.Play();
                Destroy(gameObject);
            }
        }
    }
}
