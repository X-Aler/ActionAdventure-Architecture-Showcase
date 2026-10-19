using PrimeTween;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controllers
{
    public abstract class ButtonController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        private readonly Vector3 _targetSize = new Vector3(1.2f, 1.2f, 1.2f);
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            Tween.Scale(transform, Vector3.one, _targetSize, 1);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Tween.Scale(transform, _targetSize, Vector3.one, 1);
        }

        public abstract void Click();
    }
}