using System;
using UnityEngine;
using PrimeTween;

namespace ActOne.Minigame
{
    public class Blaster : MonoBehaviour
    {
        public void Initialize(Vector2 direction, float speed)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            
            var newRay = transform.GetChild(0).gameObject;
            newRay.transform.SetParent(transform);
            
            var rect = newRay.GetComponent<RectTransform>();
            
            Tween.UISizeDelta(rect, new Vector2(rect.sizeDelta.x, 1000f), 2f).OnComplete(() => 
                Tween.UISizeDelta(rect, new Vector2(rect.sizeDelta.x, 0), 1f));
            
            Destroy(gameObject, 3);
        }
        
    }
}