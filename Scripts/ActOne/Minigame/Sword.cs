using System;
using Controllers;
using UnityEngine;

namespace ActOne.Minigame
{
    public class Sword : MonoBehaviour
    {
        private RectTransform _player;
        private float _speed;
        private RectTransform _rectTransform;

        public void Initialize(RectTransform player, float speed)
        {
            _rectTransform = GetComponent<RectTransform>();
            _player = player;
            _speed = speed;
            
            Destroy(gameObject, 3);
        }

        private void Update()
        {
            if (!_player) return;
            
            var dirToPlayer = (_player.anchoredPosition - _rectTransform.anchoredPosition).normalized;
            
            var angle = Mathf.Atan2(dirToPlayer.y, dirToPlayer.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
            
            _rectTransform.anchoredPosition += dirToPlayer * _speed * Time.deltaTime;
        }
    }
}