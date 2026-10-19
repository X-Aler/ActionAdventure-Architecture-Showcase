using System;
using Managers;
using UnityEngine;

namespace ActThree
{
    public class Qte : MonoBehaviour
    {
        [SerializeField] private QteUI qteUI;
        
        private float _time;
        private bool _active;

        private KeyCode _keyCode;

        private Action<bool> _callback;

        public void StartQte(float duration, KeyCode keyCode, Action<bool> onFinish)
        {
            _time = duration;
            _callback = onFinish;
            _keyCode = keyCode;

            _active = true;
            
            qteUI.ShowQte(_keyCode, _time);
        }

        void Update()
        {
            if (!_active) return;

            _time -= Time.deltaTime;

            if (Input.anyKeyDown)
            {
                if (Input.GetKeyDown(_keyCode))
                {
                    Finish(true);
                }
                else
                {
                    Finish(false);
                }

                return;
            }

            if (_time <= 0)
            {
                Finish(false);
            }
        }

        void Finish(bool result)
        {
            _active = false;

            qteUI.HideQte();

            AudioManager.Instance.PlayOneShot(result ? AudioManager.Instance.correctSound : AudioManager.Instance.errorSound);

            _callback?.Invoke(result);
        }
    }

}
