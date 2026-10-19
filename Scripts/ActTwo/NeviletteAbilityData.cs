using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "NeviletteAbilityData", menuName = "Scriptable Objects/AbilityData/NeviletteAbilityData", order = 0)]
    public class NeviletteAbilityData : AbilityData
    {
        public GameObject bulletPrefab;
        public GameObject zonePrefab;
        
        public float zoneDamageCooldown;
        public float zoneSize;
        public float zoneSizeSpeed;
        public float zoneStayTime;
    }
}
