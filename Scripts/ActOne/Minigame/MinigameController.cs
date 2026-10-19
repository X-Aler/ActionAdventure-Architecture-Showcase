using System;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ActOne.Minigame
{
    public class MinigameController : MonoBehaviour
    {
        [FormerlySerializedAs("chicatiloController")]
        [SerializeField] private MinigameEnemyController minigameEnemyController;
        [SerializeField] private MinigameHealthController healthController;

        [SerializeField] private AudioClip minigameSound;
        [SerializeField] private AudioClip mainBackGroundSound;
        
        [SerializeField] private TextMeshProUGUI timerText;
        [SerializeField] private int timerMinutes;
        [SerializeField] private int timerSeconds;

        [SerializeField] private GameObject minigameCamera;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject mainKey;

        public bool canEndAct = false;
        
        private Timer _timer;

        private void Awake()
        {
            _timer = new Timer();
        }
        private void OnEnable()
        {
            healthController.Death += LoseGame;
            _timer.TimerEnd += WinGame;
        }
        
        private void OnDisable()
        {
            healthController.Death -= LoseGame;
            _timer.TimerEnd -= WinGame;
            StopGame();
        }
        

        public void StartGame()
        {
            player.SetActive(false);
            
            minigameCamera.SetActive(true);
            
            gameObject.SetActive(true);
            
            healthController.Initialize();
            
            _timer.StartTimer(this, timerMinutes, timerSeconds, timerText);
            
            AudioManager.Instance.StopLoop(mainBackGroundSound);
            AudioManager.Instance.PlayLoop(minigameSound);

            minigameEnemyController.Initialize();
        }
        
        public void StopGame()
        {
            gameObject.SetActive(false);
            player.SetActive(true);
            minigameCamera.SetActive(false);
            
            AudioManager.Instance.PlayLoop(mainBackGroundSound);
            AudioManager.Instance.StopLoop(minigameSound);
        }

        private void LoseGame()
        {
            StopGame();
        }

        private void WinGame()
        {
            StopGame();
            mainKey.SetActive(true);
            canEndAct = true;
        }
    }
}