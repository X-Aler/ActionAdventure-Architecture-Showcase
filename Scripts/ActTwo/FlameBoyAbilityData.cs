using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "FlameBoyAbilityData", menuName = "Scriptable Objects/AbilityData/FlameBoyAbilityData", order = 0)]
    public class FlameBoyAbilityData : AbilityData
    {
        public GameObject bulletPrefab;
        
        public GameObject bakePrefab;
        
        public LayerMask bakeLayerMask;
        
        public float bakeSpeed;
        
        public float attractionStrength;
        public float attractionTime;
        public float attractionDistance;
    }
}
