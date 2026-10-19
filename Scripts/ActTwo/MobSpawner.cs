using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ActTwo
{
    public class MobSpawner : MonoBehaviour
    {
        [SerializeField] private SpawnData[] mobs;
        [SerializeField] private GameObject[] zones;
        [SerializeField] private int maxSpawn;
        [SerializeField] private float spawnTimer;
        
        private List<GameObject> _spawnedMobs = new List<GameObject>();
        private void Awake()
        {
            StartCoroutine(Spawner());
        }

        private IEnumerator Spawner()
        {
            while (true)
            {
                Spawn();
                yield return new WaitForSeconds(spawnTimer);
            }
        }

        private void Spawn()
        {
            RemoveNull();
            
            foreach (var zone in zones)
            {
                for (int i = 0; i < (maxSpawn / zones.Length); i++)
                {
                    foreach (var mob in mobs)
                    {
                        if (_spawnedMobs.Count >= maxSpawn) return;
                        
                        var spawnPosition = GetRandomPositionInZone(zone);
                    
                        var randomNumber = Random.Range(1, 101);

                        if (randomNumber > mob.spawnChance) continue;
                    
                        var spawnMob = Instantiate(mob.mobPrefab, spawnPosition, Quaternion.identity);
                        _spawnedMobs.Add(spawnMob);
                    }
                }
            }
        }

        private void RemoveNull()
        {
            _spawnedMobs.RemoveAll(e => e == null);
        }
        
        private Vector3 GetRandomPositionInZone(GameObject zone)
        {
            var center = zone.transform.position;
            var size = zone.transform.localScale;

            var x = Random.Range(-size.x / 2, size.x / 2);
            var y = Random.Range(-size.y / 2, size.y / 2);
            var z = Random.Range(-size.z / 2, size.z / 2);

            return center + zone.transform.TransformDirection(new Vector3(x, y, z));
        }
    }
}
