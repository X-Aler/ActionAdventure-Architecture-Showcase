using Managers;
using Other;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public enum DoorState
    {
        Nothing,
        NeedItem,
        FromOtherSide
    }
    public class OpenableController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textObject;
        [SerializeField] private ShowText needItemText;
        [SerializeField] private ShowText fromOtherSideText;
        [SerializeField] public GameObject player;
        [SerializeField] private float maxDistance;
        private TextShowManager _textShowManager;

        private void Awake()
        {
            _textShowManager = new TextShowManager(textObject, 1);
        }

        public void Open(Animator animator)
        {
            animator.SetTrigger("Open");
        }

        public void Close(Animator animator)
        {
            animator.SetTrigger("Close");
        }
        
        
        public bool TryUnlock(ItemsIDs needItemId, ItemsHandlerManager itemsHandler, GameObject objectToBreak)
        {
            if (!itemsHandler.itemEquip)
            {
                StartCoroutine(_textShowManager.ShowTextOne(needItemText));
                return false;
            }

            var currentHeldItem = itemsHandler.currentItemData;
            
            if (currentHeldItem.itemID != needItemId)
            {
                StartCoroutine(_textShowManager.ShowTextOne(needItemText));
                return false;
            }

            if (objectToBreak)
            {
                Animator objectAnimator = objectToBreak.GetComponent<Animator>();
                
                objectAnimator.SetTrigger("Break");
            }
            
            return true;
        }

        public bool TryUnlock(Transform sideTransform)
        {
            Vector3 direction = player.transform.position - sideTransform.position;
            
            
            if (direction.magnitude > maxDistance)
            {
                StartCoroutine(_textShowManager.ShowTextOne(fromOtherSideText)); 
                return false;
            }
            
            float angle = Vector3.Angle(sideTransform.forward, direction);
            
            if (angle > 45)
            {
                StartCoroutine(_textShowManager.ShowTextOne(fromOtherSideText)); 
                return false;
            }
            return true;
        }
    }
}