using UnityEngine;

namespace ActTwo
{
    public class CompassController : MonoBehaviour
    {
        [SerializeField] private Vector3 followPosition;
        [SerializeField] private Transform player;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            var toTarget = followPosition - player.position;
            toTarget.y = 0;

            var angleToTarget = Mathf.Atan2(toTarget.x, toTarget.z) * Mathf.Rad2Deg;
            
            var playerY = player.eulerAngles.y;
            
            var follow = angleToTarget - playerY;
            
            _rectTransform.rotation = Quaternion.Euler(0, 0, -follow);
        }
    }
}
