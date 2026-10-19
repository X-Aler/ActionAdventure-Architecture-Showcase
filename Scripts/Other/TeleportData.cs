using Managers;
using UnityEngine;

namespace Other
{
    [CreateAssetMenu(fileName = "TeleportData", menuName = "Scriptable Objects/TeleportData")]
    public class TeleportData : ScriptableObject
    {
        public TeleportLocations locationName;
        public Vector3 teleportLocation;
    }
}
