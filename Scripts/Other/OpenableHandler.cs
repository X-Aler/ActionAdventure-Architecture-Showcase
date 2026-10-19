using Controllers;
using Managers;
using UnityEngine;

namespace Other
{
    public class OpenableHandler : MonoBehaviour, IInteractable
    {
        [SerializeField] private OpenableController openableController;
        [SerializeField] private DoorState doorState;
        [SerializeField] private ItemsIDs needItemId;
        [SerializeField] private Transform side;
        [SerializeField] private GameObject objectToBreak;
        private bool _locked;
        
        private Animator _animator;

        private void Awake()
        {
            _locked = doorState != DoorState.Nothing;
            _animator = GetComponent<Animator>();
        }

        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            InteractActions currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Open:
                    if (_locked)
                    {
                        bool success = false;
                        switch (doorState)
                        {
                            case DoorState.NeedItem:
                                success = openableController.TryUnlock(needItemId, itemsHandler, objectToBreak);
                                break;
                            case DoorState.FromOtherSide:
                                success = openableController.TryUnlock(side); 
                                break;
                        }

                        if (success)
                        {
                            _locked = false;
                            openableController.Open(_animator);
                            data.NextAction();
                        }
                    }
                    else
                    {
                        openableController.Open(_animator);
                        data.NextAction();
                    }
                    break;

                case InteractActions.Close:
                    openableController.Close(_animator);
                    data.NextAction();
                    break;
            }
        }

        public string GetActionName(InteractActions action) => action.ToString();
    }
}