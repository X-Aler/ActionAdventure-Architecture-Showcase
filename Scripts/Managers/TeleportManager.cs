using ActTwo;
using Other;
using UnityEngine;
using UnityEngine.UI;
using PrimeTween;
namespace Managers
{
    public enum TeleportLocations
    {
        Camp,
        Forest,
        Berlin,
        Mosque,
        Gates
    }

    public class TeleportManager : MonoBehaviour
    {
        public static TeleportManager Instance;

        [SerializeField] private GameObject player;
        [SerializeField] private AudioClip driveSound;
        [SerializeField] private TeleportData[] locations;
        [SerializeField] private Image blackScreen;
        
        [SerializeField] private int telepotPrice;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        public void Teleport(TeleportLocations location)
        {
            var currentMoney = MoneyManager.Instance.GetMoney();
            
            if (currentMoney < telepotPrice) return;
            
            Tween.Alpha(blackScreen, 0f, 1f, 1f).OnComplete(() =>
            {
                AudioManager.Instance.PlayOneShot(driveSound);
                foreach (var l in locations)
                {
                    if (l.locationName != location) continue;

                    player.SetActive(false);
                    player.transform.position = l.teleportLocation;
                    player.SetActive(true);
                    break;
                }
                DialogueManager.Instance.EndDialogue();
                Tween.Alpha(blackScreen, 1f, 0f, 1f);
            });
            
            MoneyManager.Instance.RemoveMoney(telepotPrice);
        }
        
        public void Teleport(Transform location)
        {
            Tween.Alpha(blackScreen, 0f, 1f, 0.5f).OnComplete(() => Tween.Alpha(blackScreen, 1f, 0f, 1f));
            
            player.SetActive(false);
            player.transform.position = location.position;
            player.SetActive(true);
        }
        
        public void Teleport(Vector3 location)
        {
            Tween.Alpha(blackScreen, 0f, 1f, 0.5f).OnComplete(() => Tween.Alpha(blackScreen, 1f, 0f, 1f));
            
            player.SetActive(false);
            player.transform.position = location;
            player.SetActive(true);
        }
    }
}