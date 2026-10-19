using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ActThree
{
    public class ZoneArrow : MonoBehaviour
    {
        [SerializeField] private KeyCode arrowKey;
        [SerializeField] private Arrows arrow;
        [SerializeField] private GameObject arrowPrefab;

        private List<FlyArrow> _arrows = new List<FlyArrow>();
        
        private bool _initialized;

        public void StartGame()
        {
            _initialized = true;
            StartCoroutine(SpawnArrow());
        }

        public void StopGame()
        {
            _initialized = false;
            StopAllCoroutines();
        }

        private void Update()
        {
            if (!_initialized) return;

            if (Input.GetKeyDown(arrowKey))
            {
                if (_arrows.Count > 0)
                {
                    for (var i = _arrows.Count - 1; i >= 0; i--)
                    {
                        BossPhaseTwo.Instance.AddScore(1);

                        var arrow = _arrows[i];
                        _arrows.RemoveAt(i);
                        Destroy(arrow.gameObject);
                    }
                }
                else
                {
                    BossPhaseTwo.Instance.RemoveScore(1);
                }
            }
        }


        private IEnumerator SpawnArrow()
        {
            while (_initialized)
            {
                var arrowObject = Instantiate(arrowPrefab, transform.position + Vector3.down * 10, transform.rotation);
                var flyArrow = arrowObject.GetComponent<FlyArrow>();
                flyArrow.Init(BossPhaseTwo.Instance.arrowSpeed, transform);

                var spawnCd = Random.Range(0.5f, BossPhaseTwo.Instance.maxSpawnCooldown);
                
                yield return new WaitForSeconds(spawnCd);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            Check(other.gameObject, true);
        }

        private void OnTriggerExit(Collider other)
        {
            Check(other.gameObject, false);
        }

        private void Check(GameObject other, bool state)
        {
            if (!other.TryGetComponent<FlyArrow>(out var flyArrow)) return;

            if (state)
            {
                if (_arrows.Contains(flyArrow)) return;
                if (flyArrow.arrow != arrow) return;
                
                _arrows.Add(flyArrow);
            }
            else
            {
                if (!_arrows.Contains(flyArrow)) return;
                _arrows.Remove(flyArrow);
                BossPhaseTwo.Instance.RemoveScore(1);
                
                Destroy(other, 1f);
            }
        }
    }
}
