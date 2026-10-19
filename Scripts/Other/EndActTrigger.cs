using System;
using ActOne.Minigame;
using Managers;
using UnityEngine;

namespace Other
{
    public class EndActTrigger : MonoBehaviour
    {
        [SerializeField] private MinigameController minigameController;
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            if (minigameController.canEndAct)
            {
                LoadingManager.Instance.Load("ActOneEndScene");
            }
        }
    }
}
