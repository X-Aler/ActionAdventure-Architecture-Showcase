using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "BombDropAbilityData", menuName = "Scriptable Objects/AbilityData/BombDropAbilityData", order = 0)]
    public class BombDropAbilityData : AbilityData
    {
        public GameObject bulletPrefab;
        public GameObject pig;
        public GameObject explosion;

        public float explosionSize;
        public float bulletSpeed;
        public float pigSpeed;
        public float explosionSpeed;
    }
}
