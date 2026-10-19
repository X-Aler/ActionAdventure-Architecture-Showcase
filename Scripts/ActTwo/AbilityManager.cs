using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ActTwo
{
    public class AbilityManager : MonoBehaviour
    {
        public static AbilityManager Instance;
        [HideInInspector] public Ability currentAbility;

        [SerializeField] private Transform handSpawnPoint;
        [SerializeField] private Transform player;
        [FormerlySerializedAs("sanyaAbility")]
        [SerializeField] private HomingProjectileAbility homingProjectileAbility;
        [FormerlySerializedAs("alaxAbility")]
        [SerializeField] private BombDropAbility bombDropAbility;
        [SerializeField] private FlameBoyAbility flameBoyAbility;
        [SerializeField] private NeviletteAbility neviletteAbility;

        [SerializeField] private GameObject abilityInfo;
        [SerializeField] private TextMeshProUGUI shootCooldownText;
        [SerializeField] private TextMeshProUGUI specialCooldownText;
        [SerializeField] private TextMeshProUGUI abilityNameText;

        public event Action OnAttackEnd;

        private GameObject _hand;

        private bool _canAttack;

        private bool _canShoot = true;
        private bool _canSpecial = true;
        private Transform _bulletSpawnPoint;

        private Coroutine _shootCooldownCoroutine;
        private Coroutine _specialCooldownCoroutine;

        public void Initialize()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            currentAbility = null;
            
            EnemiesCounter.Instance.OnDangerChanged += SetCanAttack;
        }

        private void OnDisable()
        {
            EnemiesCounter.Instance.OnDangerChanged -= SetCanAttack;
        }

        private void Update()
        {
            if (!currentAbility) return;

            var shootType = currentAbility.data.shootType;

            switch (shootType)
            {
                case ShootType.Hold:
                {
                    if (Input.GetMouseButtonUp(0))
                    {
                        OnAttackEnd?.Invoke();

                        DestroyHand(0.1f);
                        
                        if (!_canShoot)
                        {
                            _shootCooldownCoroutine ??= StartCoroutine(Cooldown(currentAbility.data.attackCooldown,
                                shootCooldownText, () =>
                                {
                                    _canShoot = true;
                                    _shootCooldownCoroutine = null;
                                }));
                        }
                    }

                    if (!_canAttack) break;

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_canShoot)
                        {
                            _canShoot = false;
                            Attack();
                        }
                    }

                    break;
                }

                case ShootType.Single:
                {
                    if (!_canAttack) break;

                    if (Input.GetMouseButton(0))
                    {
                        if (_canShoot)
                        {
                            _canShoot = false;

                            Attack();

                            DestroyHand(currentAbility.data.attackCooldown);

                            _shootCooldownCoroutine ??= StartCoroutine(Cooldown(currentAbility.data.attackCooldown,
                                shootCooldownText, () =>
                                {
                                    _canShoot = true;
                                    _shootCooldownCoroutine = null;
                                }));
                        }
                    }

                    break;
                }
            }

            if (!_canAttack) return;
            
            if (Input.GetKeyDown(KeyCode.Q) && _canSpecial)
            {
                if (_specialCooldownCoroutine == null)
                {
                    _canSpecial = false;
                    currentAbility.Special();

                    _specialCooldownCoroutine = StartCoroutine(Cooldown(currentAbility.data.specialCooldown,
                        specialCooldownText, () =>
                        {
                            _canSpecial = true;
                            _specialCooldownCoroutine = null;
                        }));
                }
            }
        }

        private void Attack()
        {
            SpawnHand();
            currentAbility.Shoot(_bulletSpawnPoint);
        }

        public void SetCanAttack(bool attack)
        {
            _canAttack = attack;
        }

        private void SpawnHand()
        {
            if (_hand) return;

            _hand = Instantiate(currentAbility.data.handPrefab, handSpawnPoint.position, handSpawnPoint.rotation);
            _hand.transform.SetParent(handSpawnPoint);
            _bulletSpawnPoint = _hand.transform.Find("BulletSpawnPoint");
        }

        private void DestroyHand(float time)
        {
            if (!_hand) return;

            Destroy(_hand, time);
        }

        private IEnumerator Cooldown(float cooldownTime, TMP_Text cooldownText, Action onComplete)
        {
            if (cooldownTime <= 0f)
            {
                cooldownText.text = "USE";
                onComplete?.Invoke();
                yield break;
            }

            var timer = cooldownTime;

            while (timer > 0)
            {
                cooldownText.text = Mathf.Ceil(timer).ToString("F0");
                timer -= Time.deltaTime;
                yield return null;
            }

            cooldownText.text = "USE";
            onComplete?.Invoke();
        }

        public void SetAbility(Abilities ability)
        {
            if (currentAbility && currentAbility.data.ability == ability) return;

            if (currentAbility)
            {
                Destroy(currentAbility.gameObject, 0.1f);
            }

            Ability newAbility = null;

            switch (ability)
            {
                case Abilities.HomingProjectile:
                    newAbility = Instantiate(homingProjectileAbility, player);
                    break;
                case Abilities.BombDrop:
                    newAbility = Instantiate(bombDropAbility, player);
                    break;
                case Abilities.FlameBoy:
                    newAbility = Instantiate(flameBoyAbility, player);
                    break;
                case Abilities.Nevilette:
                    newAbility = Instantiate(neviletteAbility, player);
                    break;
            }

            if (!newAbility) return;
            newAbility.data.ability = ability;
            currentAbility = newAbility;

            abilityNameText.text = newAbility.data.ability.ToString();

            if (abilityInfo.activeSelf) return;
            abilityInfo.SetActive(true);
        }
    }
}