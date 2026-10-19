using ActTwo;
using Managers;
using UnityEngine;

namespace Other
{
    public class FlowerInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private Flowers flowerType;
        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            var currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Take:
                    FlowersManager.Instance.Collect(flowerType);
                    gameObject.SetActive(false);
                    break;
            }
        }

        public string GetActionName(InteractActions action)
        {
            return action.ToString();
        }
    }
}
