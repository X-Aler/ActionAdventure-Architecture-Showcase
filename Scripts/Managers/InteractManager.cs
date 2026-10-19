using System;
using System.Collections.Generic;
using ActTwo;
using Other;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class InteractManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI interactText;
        [SerializeField] private KeyCode interactKey;
        [SerializeField] private ItemsHandlerManager  itemsHandler;
        
        private List<Collider> _interactables = new List<Collider>();

        private InteractData _interactData;
        private GameObject _interactTarget;
        
        
        private bool _inZone;
        private bool _canInteract = true;
        
        private void Awake()
        {
            interactText.gameObject.SetActive(false);
            
        }

        private void OnEnable()
        {
            if (EnemiesCounter.Instance)
            {
                EnemiesCounter.Instance.OnDangerChanged += SetInteract;
            }
        }

        private void OnDisable()
        {
            if (EnemiesCounter.Instance)
            {
                EnemiesCounter.Instance.OnDangerChanged -= SetInteract;
            }
            
            _interactables.Clear();
            
            _interactTarget = null;
            _interactData = null;
            
            if (!interactText) return; 
                
            interactText.gameObject.SetActive(false);
            interactText.text = "";
        }

        private void Update()
        {
            if (!_interactTarget || !_interactData || !Input.GetKeyDown(interactKey) || !_canInteract) return;
            
            var interactable = _interactTarget.GetComponent<IInteractable>();
            
            if (interactable == null) return;
            
            var currentAction = _interactData.GetCurrentAction();
            interactable.Interact(_interactData, itemsHandler);
            
            if (_interactTarget == null || !_interactTarget.activeInHierarchy)
            {
                UpdateTarget();
                return;
            }
        
            AudioManager.Instance.PlayOneShot(currentAction.actionSound);
        
            if (!_interactData) return;
            
            var nextAction = _interactData.GetCurrentAction();
            interactText.text = $"{_interactData.objectName} \n {interactKey} - {nextAction.actionName}";
        }

        private void SetInteract(bool interact)
        {
            _canInteract = !interact;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            _interactables.Add(other);
            UpdateTarget();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Interactable")) return;
            
            _interactables.Remove(other);
            UpdateTarget();
        }

        private void UpdateTarget()
        {
            _interactables.RemoveAll(c => c == null || !c.gameObject.activeInHierarchy);
            
            if (_interactables.Count == 0)
            {
                _interactData = null;
                _interactTarget = null;
                interactText.text = "";
                interactText.gameObject.SetActive(false);
                return;
            }

            Collider nearest = null;
            var maxPriority = int.MinValue;

            foreach (var c in _interactables)
            {
                _interactData = c.GetComponent<InteractData>();
                var priority = _interactData.showPriority;
                
                if (priority <= maxPriority) continue;
                
                maxPriority = priority;
                nearest = c;
            }

            if (nearest != null)
            {
                ShowItem(nearest);
            }
        }

        private void ShowItem(Collider other)
        {
            _interactTarget = other.gameObject;
            _interactData = other.GetComponent<InteractData>();
            
            var currentAction = _interactData.GetCurrentAction();
            
            interactText.gameObject.SetActive(true);
            interactText.text = $"{_interactData.objectName} \n {interactKey} - {currentAction.actionName}";
        }
    }
}
