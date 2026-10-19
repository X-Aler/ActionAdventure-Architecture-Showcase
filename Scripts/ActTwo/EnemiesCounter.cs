using System;
using System.Collections.Generic;
using Controllers;
using Managers;
using UnityEngine;

namespace ActTwo
{
    public class EnemiesCounter : MonoBehaviour
    {
        public static EnemiesCounter Instance;

        public event Action<bool> OnDangerChanged;

        [SerializeField] public GameObject dangerText;
        [SerializeField] public AudioClip dangerSound;
        [SerializeField] public AbilityManager abilityManager;
        [SerializeField] private HealthController playerHealth;

        private readonly List<GameObject> _enemies = new List<GameObject>();

        private bool _inDanger;
        private bool _isBoss;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            
            abilityManager.Initialize();
        }

        private void Start()
        {
            UpdateAttackStatus();
        }

        public void SetBoss(bool value)
        {
            _isBoss = value;
            SetDanger(value);
        }

        public void SetDanger(bool value)
        {
            _inDanger = value;
            UpdateStatus();
        }
        
        public void ResetDanger()
        {
            _enemies.Clear();
            SetDanger(false);
        }

        public GameObject GetNearestEnemy()
        {
            GameObject nearest = null;
            var minSqrDistance = float.MaxValue;

            RemoveNull();

            foreach (var e in _enemies)
            {
                var sqrDistance = (transform.position - e.transform.position).sqrMagnitude;

                if (!(sqrDistance < minSqrDistance)) continue;

                minSqrDistance = sqrDistance;
                nearest = e;
            }

            return nearest;
        }

        private bool CanAttack() => _enemies.Count > 0;

        public int GetEnemiesCount() => _enemies.Count;

        public void RemoveEnemy(GameObject enemy)
        {
            if (!_enemies.Contains(enemy) || _isBoss) return;
            _enemies.Remove(enemy);
            UpdateAttackStatus();
        }

        private void AddEnemy(GameObject enemy)
        {
            if (_enemies.Contains(enemy) || _isBoss) return;
            _enemies.Add(enemy);
            UpdateAttackStatus();
        }

        private void RemoveNull()
        {
            _enemies.RemoveAll(e => e is null);
        }

        public void UpdateAttackStatus()
        {
            RemoveNull();
            
            SetDanger(CanAttack());

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            RemoveNull();
            
            OnDangerChanged?.Invoke(_inDanger);
            dangerText.SetActive(_inDanger);
            
            if (_inDanger)
            {
                AudioManager.Instance.PlayOneShot(dangerSound);
            }

        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Enemy")) return;

            AddEnemy(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            RemoveEnemy(other.gameObject);
        }
    }
}