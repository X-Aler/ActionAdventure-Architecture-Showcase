using ActTwo;
using Managers;
using UnityEngine;

namespace Other
{
    public class TeleportButton : MonoBehaviour
    {
        [SerializeField] private TeleportLocations locationName;

        public void OnClick()
        {
            TeleportManager.Instance.Teleport(locationName);
        }
    }
}
