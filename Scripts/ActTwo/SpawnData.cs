using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
    public class SpawnData : ScriptableObject
    {
        public GameObject mobPrefab;
        public int spawnChance;
    }
}
