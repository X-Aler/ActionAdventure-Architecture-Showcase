using System;
using TMPro;
using UnityEngine;
using PrimeTween;

namespace ActThree
{
    public class QteUI : MonoBehaviour
    {
        [SerializeField] private GameObject qteButton;

        private TextMeshProUGUI _buttonText;
        private Tween _currentTween;
        private void Awake()
        {
            _buttonText = qteButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        public void ShowQte(KeyCode key, float time)
        {
            qteButton.transform.localScale = Vector3.one;
            
            _buttonText.text = key.ToString();
            
            qteButton.SetActive(true);
            
            _currentTween = Tween.Scale(qteButton.transform, Vector3.zero, time).OnComplete(() => HideQte());
        }

        public void HideQte()
        {
            if (_currentTween.isAlive)
            {
                _currentTween.Stop();
            }
            
            qteButton.SetActive(false); 
        }
    }
}
