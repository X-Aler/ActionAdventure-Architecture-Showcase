using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace ActOne.Minigame
{
    public class Timer
    {
        public event Action TimerEnd;
        
        private Coroutine _timerCoroutine;
        
        public void StartTimer(MonoBehaviour timerStarter, int minutes, int seconds, TextMeshProUGUI text)
        {
            if (_timerCoroutine != null)
                timerStarter.StopCoroutine(_timerCoroutine);

            _timerCoroutine = timerStarter.StartCoroutine(TimerCoroutine(minutes, seconds, text)); 
        }

        private IEnumerator TimerCoroutine(int minutes, int seconds, TextMeshProUGUI text)
        {
            while (minutes > 0 || seconds > 0)
            {
                text.text = $"{minutes:D2}:{seconds:D2}";
                yield return new WaitForSeconds(1f);

                if (seconds == 0)
                {
                    if (minutes > 0)
                    {
                        minutes--;
                        seconds = 59;
                    }
                }
                else
                {
                    seconds--;
                }
            }

            text.text = "00:00";
            TimerEnd?.Invoke();
        }
    }
}