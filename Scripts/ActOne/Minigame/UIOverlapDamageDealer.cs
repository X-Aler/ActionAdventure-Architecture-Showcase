using UnityEngine;

namespace ActOne.Minigame
{
    public class UIOverlapDamageDealer : MonoBehaviour
    {
        private RectTransform _player;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _player = FindAnyObjectByType<MinigameHealthController>().gameObject.GetComponent<RectTransform>();
        }

        private void Update()
        {
            if (!_player|| !_rectTransform) return;

            if (UIExtensions.IsOverlapping(_player, _rectTransform))
            {
                var health = _player.GetComponent<MinigameHealthController>();
                if (health)
                {
                    health.TakeDamage();
                }

                Destroy(gameObject);
            }
        }
    }
}