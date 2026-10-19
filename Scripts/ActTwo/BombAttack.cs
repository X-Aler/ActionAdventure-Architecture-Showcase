using UnityEngine;
using UnityEngine.Playables;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "BombAttack", menuName = "Scriptable Objects/BossAttack/BombAttack")]
    public class BombAttack : BossAttack
    {
        public GameObject prefab;
        public Transform spawnPoint;
        
        public float timer;

        public float explosionSize;
        public float explosionSpeed;       
        
    }
}
