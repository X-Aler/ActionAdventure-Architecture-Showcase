using System.Collections;
using PrimeTween;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class TextShowManager 
    {
        private readonly TextMeshProUGUI _mainText;
        private readonly float _fadeDuration;

        private ShowText _currentShowText;
        public TextShowManager(TextMeshProUGUI mainText, float fadeDuration)
        {
            _mainText = mainText;
            _fadeDuration = fadeDuration;
        }

        public IEnumerator ShowText(ShowText showText)
        {
            if (TextCheck(showText)) yield break;
            _mainText.alpha = 0f;

            for (int i = 0; i < showText.textToShow.Length; i++)
            {
                _mainText.text = showText.textToShow[i];

                PlaySound(showText, i);

                Tween.Alpha(_mainText, 0, 1, _fadeDuration);
                yield return new WaitForSeconds(_fadeDuration);

                Tween.Alpha(_mainText, 1, 0, _fadeDuration);
                yield return new WaitForSeconds(_fadeDuration);
            }
            _currentShowText = null;
        }

        public IEnumerator ShowTextOne(ShowText showText)
        {
            if (TextCheck(showText)) yield break;
            _mainText.alpha = 0f;

            if (showText.textToShow.Length > 0)
                _mainText.text = showText.textToShow[0];

            PlaySound(showText, 0);

            Tween.Alpha(_mainText, 0, 1, _fadeDuration);
            yield return new WaitForSeconds(_fadeDuration);

            Tween.Alpha(_mainText, 1, 0, _fadeDuration);
            yield return new WaitForSeconds(_fadeDuration);
            _currentShowText = null;
        }

        private void PlaySound(ShowText showText, int index)
        {
            if (showText.textSound == null) return;

            AudioManager.Instance.PlayOneShot(showText.textSound.Length > index
                ? showText.textSound[index]
                : showText.textSound[0]);
        }

        private bool TextCheck(ShowText currentShowText)
        {
            if (!_currentShowText)
            {
                _currentShowText = currentShowText;
                return false;
            }
            else
            {
                return _currentShowText == currentShowText;
            }
        }
    }
}