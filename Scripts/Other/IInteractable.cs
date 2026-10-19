using Managers;

namespace Other
{
    public interface IInteractable
    {
        void Interact(InteractData data, ItemsHandlerManager itemsHandler);
        string GetActionName(InteractActions action);
    }
}