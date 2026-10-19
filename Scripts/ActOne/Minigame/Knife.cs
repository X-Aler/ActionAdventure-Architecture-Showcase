using System;
using UnityEngine;

namespace ActOne.Minigame
{
    public class Knife : MonoBehaviour
    {
        private Vector2 _targetDirection;
        private float _speed;
        private RectTransform _rectTransform;

        public void Initialize(Vector2 direction, float speed)
        {
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);

            _rectTransform = GetComponent<RectTransform>();
            _targetDirection = direction;
            _speed = speed;
            
            Destroy(gameObject, 5);
        }

        private void Update()
        {
            if (_targetDirection == Vector2.zero) return;

            _rectTransform.anchoredPosition += _targetDirection * _speed * Time.deltaTime;
        }
    }
}