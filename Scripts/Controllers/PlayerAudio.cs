using System;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class PlayerAudio : MonoBehaviour
    {
        [SerializeField] private AudioClip walkSound;
        [SerializeField] private AudioClip runSound;
       
        private AudioClip _currentClip;
        private bool _isPlayingSound = false;
        
        public void UpdateSound(bool isMoving, bool isRunning)
        {
            if (isMoving)
            {
                var targetClip = isRunning ? runSound : walkSound;

                if (_isPlayingSound && _currentClip == targetClip) return;
                
                StopSound();
                
                AudioManager.Instance.PlayLoop(targetClip);
                _currentClip = targetClip;
                _isPlayingSound = true;
            }
            else
            {
                if (_isPlayingSound)
                {
                    StopSound();
                }
            }
        }

        private void StopSound()
        {
                
            if (_currentClip)
            {
                AudioManager.Instance.StopLoop(_currentClip);
            }
                    
            _currentClip = null;
            _isPlayingSound = false;
        }

        private void OnDisable()
        {
            StopSound();
        }
    }
}
