using System;
using Controllers;
using Managers;
using UnityEngine;

namespace Other
{
    [System.Serializable]
    public class TurnObjectData
    {
        public GameObject target;
        public Vector3 targetScale;
        public bool destroyAfter;
        public bool isSpecialTurn;
    }
    public class TurnInteractable : MonoBehaviour, IInteractable
    {
        [SerializeField] private TurnableController turnableController;
        [SerializeField] private TurnObjectData[] turnObjectsData;
        
        private Animator _animator;
        
        private bool _isTurned;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void Interact(InteractData data, ItemsHandlerManager itemsHandler)
        {
            var currentAction = data.GetCurrentAction().action;
            switch (currentAction)
            {
                case InteractActions.Turn:
                    if (_isTurned) return;
                    turnableController.Turn(_animator, turnObjectsData);
                    _isTurned = true;
                    break;
            }
        }

        public string GetActionName(InteractActions action)
        {
            return action.ToString();
        }
    }
}
