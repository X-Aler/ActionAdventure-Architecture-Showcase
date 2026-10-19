using System;
using UnityEngine;

namespace ActOne.Minigame
{
    public class MinigamePlayerController : MonoBehaviour
    {
        [SerializeField] private RectTransform limitArea;
        private RectTransform _characterRect;
        public float speed;

        private void Awake()
        {
            _characterRect = GetComponent<RectTransform>();
        }

        private void Update()
        {
            Move();
            ClampLocation();
        }

        private void Move()
        {
            var x = Input.GetAxis("Horizontal");
            var y = Input.GetAxis("Vertical");    
            
            var move = new Vector2(x, y).normalized;
            
            transform.Translate(move * speed * Time.deltaTime);
        }

        private void ClampLocation()
        {
            var pos = transform.localPosition;
            
            var halfWidth = limitArea.rect.width / 2;
            var halfHeight = limitArea.rect.height / 2;

            var charHalfWidth = _characterRect.rect.width / 2;
            var charHalfHeight = _characterRect.rect.height / 2;
            
            pos.x = Mathf.Clamp(pos.x, -halfWidth + charHalfWidth, halfWidth - charHalfWidth);
            pos.y = Mathf.Clamp(pos.y, -halfHeight + charHalfHeight, halfHeight - charHalfHeight);

            transform.localPosition = pos;
        }
    }
}
