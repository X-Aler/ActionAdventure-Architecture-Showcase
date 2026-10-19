using ActTwo;
using Managers;
using Other;
using UnityEngine;

public class Gates : MonoBehaviour, IInteractable
{
    public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
    {
        var currentAction = data.GetCurrentAction().action;
        switch (currentAction)
        {
            case InteractActions.Open:
                var chains = ChainManager.Instance.GetChains();
                
                if (chains > 0) return;
                
                LoadingManager.Instance.Load("Act2EndScene");
                
                break;
        }
    }

    public string GetActionName(InteractActions action)
    {
        return action.ToString();
    }
}
