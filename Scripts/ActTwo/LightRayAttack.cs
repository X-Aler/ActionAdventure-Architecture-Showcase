using UnityEngine;

namespace ActTwo
{
    [CreateAssetMenu(fileName = "LightRayAttack", menuName = "Scriptable Objects/BossAttack/LightRayAttack")]
    public class LightRayAttack : BossAttack
    {
        public GameObject prefab;
        public GameObject warnObject;
        public GameObject lightScreen;
        public float lightTime;
        public float rayTime;
        public float warnTime;
        public float raySizeTime;
        public Vector3 raySize;
    }
}
