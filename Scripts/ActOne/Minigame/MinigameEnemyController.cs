using System;
using System.Collections;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace ActOne.Minigame
{
    public class MinigameEnemyController : MonoBehaviour
    {
        [SerializeField] private GameObject knifePrefab;
        [SerializeField] private GameObject blasterPrefab;
        [SerializeField] private GameObject swordPrefab;
        [SerializeField] private RectTransform player;
        [SerializeField] private RectTransform[] positions;
        
        [SerializeField] private float speed;
        [SerializeField] private float knifeSpeed;
        [SerializeField] private float swordSpeed;

        [SerializeField] private int spawn;
        [SerializeField] private float cooldown;
        
        [SerializeField] private AudioClip swordSound;
        [SerializeField] private AudioClip blasterSound;
        [SerializeField] private AudioClip knifeSound;
        
        [FormerlySerializedAs("chicatiloSounds")]
        [SerializeField] private AudioClip[] attackSounds;
        
        private Vector3 _dirToPlayer;

        private RectTransform _rectTransform;

        private int _cycle;

        private int _currentMaxSpawn;
        private int _currentMinSpawn;
        private float _currentCooldown;
        private float _currentSpeed;
        public void Initialize()
        {
            _rectTransform = GetComponent<RectTransform>();
            
            _currentMaxSpawn = spawn;
            _currentMinSpawn = 1;
            _currentCooldown = cooldown;
            _currentSpeed = speed;
            
            StartCoroutine(MainAI());
        }

        public void OnDisable()
        {
            StopAllCoroutines();
        }

        private void Update()
        {
            if (!_rectTransform) return;
            
            _dirToPlayer = (player.anchoredPosition - _rectTransform.anchoredPosition).normalized;
        }

        private int RandomNum(int minDiapason, int maxDiapason)
        {
            var num = Random.Range(minDiapason, maxDiapason);
            return num;
        }

        private IEnumerator MainAI()
        {
            _cycle = 0;
            while (true)
            {
                var idx = RandomNum(0, positions.Length);
                var targetPosition = positions[idx].anchoredPosition;
                
                
                while (Vector2.Distance(_rectTransform.anchoredPosition, targetPosition) > 1f)
                {
                    var direction = (targetPosition - _rectTransform.anchoredPosition).normalized;

                    _rectTransform.anchoredPosition += direction * _currentSpeed * Time.deltaTime;

                    yield return null;
                }
                
                _rectTransform.anchoredPosition = targetPosition;
                
                yield return new WaitForSeconds(2f);
                
                var randomNum = RandomNum(0, 3);
                switch (randomNum)
                {
                    case 0:
                        StartCoroutine(SummonWeapon(knifePrefab, RandomNum(_currentMinSpawn, _currentMaxSpawn), knifeSpeed, _currentCooldown));
                        break;
                    case 1:
                        StartCoroutine(SummonWeapon(blasterPrefab, RandomNum(_currentMinSpawn, _currentMaxSpawn), 5, _currentCooldown)); 
                        break;
                    case 2:
                        StartCoroutine(SummonWeapon(swordPrefab, RandomNum(_currentMinSpawn, _currentMaxSpawn), swordSpeed, _currentCooldown));
                        break;
                }

                _cycle++;
                
                if (_cycle == 3)
                {
                    AudioManager.Instance.PlayOneShot(attackSounds[RandomNum(0, attackSounds.Length)]);
                    _currentCooldown -= 0.1f;
                    _currentSpeed += 10;
                }
                else if (_cycle == 6)
                {
                    AudioManager.Instance.PlayOneShot(attackSounds[RandomNum(0, attackSounds.Length)]);
                    _currentMaxSpawn++;
                    _currentMinSpawn++;
                    _cycle = 0;
                }
            }
        }
        
        private IEnumerator SummonWeapon(GameObject prefab, float count = 1f, float flySpeed = 5f, float cooldown = 1f)
        {
            for (int i = 0; i < count; i++)
            {
                var obj = Instantiate(prefab, transform.parent);
                var rect = obj.GetComponent<RectTransform>();
                
                rect.anchoredPosition = _rectTransform.anchoredPosition;

                if (obj.TryGetComponent(out Knife knife))
                {
                    AudioManager.Instance.PlayOneShot(knifeSound);
                    knife.Initialize(_dirToPlayer, flySpeed);
                }
                else if (obj.TryGetComponent(out Blaster blaster))
                {
                    AudioManager.Instance.PlayOneShot(blasterSound);
                    blaster.Initialize(_dirToPlayer, flySpeed);
                }
                else if (obj.TryGetComponent(out Sword sword))
                {
                    AudioManager.Instance.PlayOneShot(swordSound);
                    sword.Initialize(player, flySpeed);
                }
                
                yield return new WaitForSeconds(cooldown);
            }
        }
    }
}
