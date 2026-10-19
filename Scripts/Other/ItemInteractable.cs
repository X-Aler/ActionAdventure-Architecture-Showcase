using Managers;
using UnityEngine;

namespace Other
{
    public class ItemInteractable : MonoBehaviour, IInteractable
    {
        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            InteractActions currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Take:
                    itemsHandler.TakeItem(gameObject);
                    break;
                case InteractActions.Throw:
                    itemsHandler.ThrowItem();
                    break;
            }
            data.NextAction();
        }
    

        public string GetActionName(InteractActions action)
        {
            return action.ToString();
        }
    }
}