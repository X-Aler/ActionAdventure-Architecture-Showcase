using System;
using Controllers;
using Managers;
using TMPro;
using UnityEngine;

namespace ActThree
{
    public enum Arrows
    {
        Left,
        Right,
        Up,
        Down,
    }
    public class BossPhaseTwo : MonoBehaviour
    {
        public static BossPhaseTwo Instance;
        
        public float arrowSpeed;
        public float maxSpawnCooldown;
        
        [SerializeField] private int scoreToWin;
        [SerializeField] private HealthController healthController;
        [SerializeField] private ZoneArrow[] zoneArrows;
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private GameObject arrows;
        
        private int _score;

        private void Awake()
        {
            Instance = this;
        }

        public void StartGame()
        {
            arrows.SetActive(true);
            foreach (var z in zoneArrows)
            {
                z.StartGame();
            }
            scoreText.gameObject.SetActive(true);
        }

        public void AddScore(int score)
        {
            _score += score;
            UpdateScore(true);
        }
        
        public void RemoveScore(int score)
        {
            if (_score >= score)
            {
                _score -= score;
            }
            else
            {
                _score = 0;
            }

            UpdateScore(false);
        }

        private void UpdateScore(bool result)
        {
            AudioManager.Instance.PlayOneShot(result ? AudioManager.Instance.correctSound : AudioManager.Instance.errorSound);
            scoreText.text = $"{_score} / {scoreToWin}";
        }

        public void EndGame()
        {
            scoreText.gameObject.SetActive(false);
            
            foreach (var z in zoneArrows)
            {
                z.StopGame();
            }
            
            arrows.SetActive(false);
            
            if (_score < scoreToWin)
            {
                healthController.TakeDamage(100);
            }
        }
    }
}
