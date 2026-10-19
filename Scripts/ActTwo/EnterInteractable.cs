using Managers;
using Other;
using UnityEngine;

namespace ActTwo
{
    public class EnterInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Transform enterPoint;
        [SerializeField] private Transform exitPoint;
        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            var currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Enter:
                    TeleportManager.Instance.Teleport(enterPoint);
                    break;
                case InteractActions.Exit:
                    TeleportManager.Instance.Teleport(exitPoint);
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
