using System.Collections;
using Controllers;
using UnityEngine;
using UnityEngine.Playables;

namespace ActTwo
{
    public enum BossAttackType
    {
        Shoot,
        AttackOne,
        AttackTwo,
        PhaseAttack,
        Ultimate
    }

    public abstract class Boss : MonoBehaviour
    {
        [SerializeField] private GameObject stunText;
        [SerializeField] private GameObject bossHealthUI;
        
        [SerializeField] private GameObject chain;

        [SerializeField] protected float maxTime = 10f;
        [SerializeField] protected float minTime = 5f;
        [SerializeField] protected int maxCycles = 5;
        [SerializeField] protected int minCycles = 2;

        public GameObject player;

        public BossAttack[] attacks;

        [SerializeField] protected PlayableDirector ultimateCutscene;

        public EnemyHealth health;

        private bool _stunned;
        private int _cycleCount;
        private int _needCycles;

        private bool _variant;
        private bool _initialized;

        private bool _canPhaseAttack;

        private GameObject _enemiesCounter;

        private Coroutine _stunCoroutine;
        private Coroutine _bossCoroutine;
        private Coroutine _shootCoroutine;


        private void TryAttack(System.Action attack)
        {
            if (_stunned) return;
            attack();
        }


        public void Initialize()
        {
            health = GetComponent<EnemyHealth>();

            _enemiesCounter = EnemiesCounter.Instance.gameObject;
            EnemiesCounter.Instance.SetBoss(true);
            _enemiesCounter.SetActive(false);

            _canPhaseAttack = false;

            health.OnHealthChanged += OnHealthChange;
            player.GetComponent<HealthController>().OnDeath += End;

            _needCycles = Random.Range(minCycles, maxCycles + 1);
            
            health.Heal(health.maxHealth);
            
            bossHealthUI.SetActive(true);

            _shootCoroutine = StartCoroutine(Shooting());
            _bossCoroutine = StartCoroutine(BossLogic());

            AfterInitialize();

            _initialized = true;
        }

        protected virtual void AfterInitialize() {}
        
        private void OnDestroy()
        {
            End();
            player.GetComponent<HealthController>().OnDeath -= End;
            Destroy(chain, 0.1f);
        }

        private void End()
        {
            if (!_initialized) return;

            if (health)
                health.OnHealthChanged -= OnHealthChange;

            if (_enemiesCounter)
                _enemiesCounter.SetActive(true);
            
            EnemiesCounter.Instance.SetBoss(false);
            
            bossHealthUI.SetActive(false);

            if (_shootCoroutine != null)
            {
                StopCoroutine(_shootCoroutine);
            }
            
            if (_bossCoroutine != null)
            {
                StopCoroutine(_bossCoroutine);
            }
        }


        private IEnumerator BossLogic()
        {
            while (true)
            {
                if (_stunned)
                {
                    yield return null;
                    continue;
                }

                if (_variant)
                {
                    TryAttack(AttackOne);
                    yield return new WaitForSeconds(GetAttackData<BossAttack>(BossAttackType.AttackOne).cooldown);
                }
                else
                {
                    TryAttack(AttackTwo);
                    yield return new WaitForSeconds(GetAttackData<BossAttack>(BossAttackType.AttackTwo).cooldown);
                }

                _variant = !_variant;

                if (_canPhaseAttack)
                {
                    TryAttack(PhaseAttack);
                    yield return new WaitForSeconds(GetAttackData<BossAttack>(BossAttackType.PhaseAttack).cooldown);
                }
                
                _cycleCount++;

                if (_cycleCount < _needCycles)
                    continue;

                var stunTime = Random.Range(minTime, maxTime + 1);

                ApplyStun(stunTime);

                yield return new WaitForSeconds(stunTime);

                _needCycles = Random.Range(minCycles, maxCycles + 1);
                _cycleCount = 0;
            }
        }


        public void ApplyStun(float duration)
        {
            if (_stunCoroutine != null)
                StopCoroutine(_stunCoroutine);
            
            _stunned = true;
            stunText.SetActive(true);
            
            _stunCoroutine = StartCoroutine(StunCoroutine(duration));
        }


        private IEnumerator StunCoroutine(float duration)
        {
            yield return new WaitForSeconds(duration);

            _stunned = false;
            stunText.SetActive(false);

            _stunCoroutine = null;
        }


        private IEnumerator Shooting()
        {
            while (true)
            {
                while (_stunned)
                    yield return null;

                TryAttack(Shoot);

                yield return new WaitForSeconds(GetAttackData<BossAttack>(BossAttackType.Shoot).cooldown);
            }
        }


        private void OnHealthChange(float currentHealth, float maxHealth)
        {
            if (_canPhaseAttack || !(currentHealth <= maxHealth * 0.5f)) return;

            Ultimate();

            _canPhaseAttack = true;
        }


        protected T GetAttackData<T>(BossAttackType type) where T : BossAttack
        {
            foreach (var a in attacks)
                if (a.attackType == type)
                    return a as T;

            return null;
        }


        protected abstract void Shoot();
        protected abstract void AttackOne();
        protected abstract void AttackTwo();
        protected abstract void PhaseAttack();
        protected abstract void Ultimate();
    }
}
