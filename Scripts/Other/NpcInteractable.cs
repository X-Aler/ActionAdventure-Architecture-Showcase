using System;
using Managers;
using UnityEngine;

namespace Other
{
    public class NpcInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private DialogueData[] dialogueDates;
        private int _dialogues;
        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            var currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Talk:
                    var currentDialogue = dialogueDates[_dialogues];
                    DialogueManager.Instance.StartDialogue(currentDialogue);
                    
                    if (currentDialogue.isOnetime)
                    {
                        _dialogues++;
                    }
                    break;
            }
        }

        public string GetActionName(InteractActions action)
        {
            return action.ToString();
        }
    }
}
