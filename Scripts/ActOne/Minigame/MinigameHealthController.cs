using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ActOne.Minigame
{
    public class MinigameHealthController : MonoBehaviour
    {
        public event Action Death;
        [SerializeField] private int maxLives;
        [SerializeField] private GameObject heartPrefab;
        [SerializeField] private RectTransform heartSpawn;
        [SerializeField] private float spawnStep;
        
        private Stack<GameObject> _heartPool = new Stack<GameObject>();

        private int _lives;
        private bool _canDamage;
        
        private Coroutine _coroutine;
        public void Initialize()
        {
            _heartPool.Clear();
            _lives = maxLives;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(Iframe());
            
            var pos = heartSpawn.anchoredPosition;
            for (int i = 0; i < _lives; i++)
            {
                var heart = Instantiate(heartPrefab, heartSpawn.parent);
                var rt = heart.GetComponent<RectTransform>();
                
                rt.anchoredPosition = pos;

                pos.x += spawnStep;

                _heartPool.Push(heart);
            }
        }
        
        public void TakeDamage()
        {
            if (!_canDamage) return;
            
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            _coroutine = StartCoroutine(Iframe());
            
            _lives--;
            Destroy(_heartPool.Pop());

            if (_lives <= 0)
            {
                Death?.Invoke();
            }
        }

        private IEnumerator Iframe()
        {
            _canDamage = false;
            yield return new WaitForSeconds(1f);
            _canDamage = true;
        }
    }
}
