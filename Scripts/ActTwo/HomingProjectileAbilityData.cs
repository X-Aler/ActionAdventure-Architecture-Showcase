using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "HomingProjectileAbilityData", menuName = "Scriptable Objects/AbilityData/HomingProjectileAbilityData", order = 0)]
    public class HomingProjectileAbilityData : AbilityData
    {
        public GameObject bulletPrefab;
        public GameObject flyPrefab;
        
        public float bulletSpeed;
        public float flySpeed;
    }
}
