using System.Collections;
using UnityEngine;
using PrimeTween;
using TMPro;

namespace ActThree
{
    public class OsuButton : MonoBehaviour
    {
        [SerializeField] private GameObject circlePrefab;

        private float _clickTime;

        private bool _clicked;
        
        private Tween _tween;

        public void Initialize(float clickTime, int clickCount)
        {
            _clickTime = clickTime;
            
            var circle = Instantiate(circlePrefab, transform);
            _tween = Tween.Scale(circle.transform, transform.localScale * 2, transform.localScale, clickTime);
            
            var numText = gameObject.GetComponentInChildren<TextMeshProUGUI>();
            numText.text = clickCount.ToString();

            StartCoroutine(Timer());
        }

        public void Click()
        {
            if (_tween.isAlive)
            {
                _tween.Stop();
            }
            
            Osu.Instance.Win();
            
            Destroy(gameObject);
        }

        private IEnumerator Timer()
        {
            yield return new WaitForSeconds(_clickTime);

            if (_clicked) yield break;
            
            Osu.Instance.Lose();
                
            Destroy(gameObject);
        }
    }
}
