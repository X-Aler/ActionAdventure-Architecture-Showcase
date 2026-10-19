using Other;
using UnityEngine;

namespace Managers
{
    public class ItemsHandlerManager : MonoBehaviour
    {
        [SerializeField] private float throwForce = 2;
        
        [HideInInspector] public InteractData currentItemData;

        public bool itemEquip;

        private GameObject _currentItem;
        private Rigidbody _itemRigidbody;
        private InteractData _currentInteractData;
        
        private FixedJoint _joint;
        
        public void TakeItem(GameObject item)
        {
            if (itemEquip)
            {
                ThrowItem();
            }
            
            if (_currentItem) return;
            
            _itemRigidbody = item.GetComponent<Rigidbody>();
            
            _currentInteractData = item.GetComponent<InteractData>();

            _currentInteractData.showPriority = 0;
            
            currentItemData = _currentInteractData;
            itemEquip = true;
            
            item.transform.SetParent(transform);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;
            
            _currentItem = item;
            
            _joint = _currentItem.AddComponent<FixedJoint>();
            
            _joint.connectedBody = GetComponent<Rigidbody>();
            
            _itemRigidbody.useGravity = false;
        }
        
        public void ThrowItem()
        {
            if (!_currentItem) return;
            
            _currentItem.transform.SetParent(null);
            
            Destroy(_joint);
            
            _itemRigidbody.useGravity = true;

            _currentInteractData.showPriority = 2;
            
            _itemRigidbody.AddForce(transform.forward * throwForce, ForceMode.Impulse);
            
            _itemRigidbody = null;
            _currentItem = null;
            _currentInteractData = null;
            
            currentItemData = null;
            itemEquip = false;
        }
        
    }
}
