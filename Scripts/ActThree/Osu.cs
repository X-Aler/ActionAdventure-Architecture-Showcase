using System;
using System.Collections;
using Controllers;
using Managers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ActThree
{
    public class Osu : MonoBehaviour
    {
        public static Osu Instance;
        
        [SerializeField] private RectTransform osuZone;
        [SerializeField] private GameObject osuButtonPrefab;
        [SerializeField] private HealthController healthController;

        private float _maxSpawnTime;
        private float _minSpawnTime;
        
        private float _maxClickTime;
        private float _minClickTime;
        
        private int _osuCount;

        private void Awake()
        {
            Instance = this;
        }

        public void StartGame(float maxSpawnTime, float minSpawnTime, float maxClickTime, float minClickTime)
        {
            _maxSpawnTime = maxSpawnTime;
            _minSpawnTime = minSpawnTime;
            
            _maxClickTime = maxClickTime;
            _minClickTime = minClickTime;
            
            CursorController.Confine();
            
            StartCoroutine(Activate());
        }

        public void StopGame()
        {
            StopAllCoroutines();
        }

        public void Win()
        {
            _osuCount++;
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.correctSound);
        }
        
        public void Lose()
        {
            _osuCount++;
            AudioManager.Instance.PlayOneShot(AudioManager.Instance.errorSound);
            healthController.TakeDamage(15);
        }

        private IEnumerator Activate()
        {
            while (true)
            {
                SpawnButton();
                yield return new WaitForSeconds(Random.Range(_minSpawnTime, _maxSpawnTime));
            }
        }

        private void SpawnButton()
        {
            var spawnPos = GetRandomPositionInZone(osuZone.rect);
            
            var button = Instantiate(osuButtonPrefab, osuZone.transform);
            button.GetComponent<RectTransform>().anchoredPosition = spawnPos;
            
            var osuButton = button.GetComponent<OsuButton>();
            osuButton.Initialize(Random.Range(_minClickTime, _maxSpawnTime), _osuCount);
        }

        private Vector2 GetRandomPositionInZone(Rect zone)
        {
            var positionX = zone.width / 2;
            var positionY = zone.height / 2;

            var randomX = Random.Range(-positionX, positionX);
            var randomY = Random.Range(-positionY, positionY);
            
            var newPosition = new Vector2(randomX, randomY);
            
            return newPosition;
        }
    }
}
