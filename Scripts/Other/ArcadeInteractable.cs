using System;
using ActOne.Minigame;
using Controllers;
using Managers;
using UnityEngine;

namespace Other
{
    public class ArcadeInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private MinigameController minigameController;

        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            var currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Play:
                    minigameController.StartGame();
                    break;
            }
        }

        public string GetActionName(InteractActions action)
        {
            return action.ToString();
        }
    }
}