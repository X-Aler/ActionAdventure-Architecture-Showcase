using Other;
using PrimeTween;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

namespace Managers
{
    public class CutsceneManager : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject cutsceneCamera;
        [SerializeField] private Image blackScreen;
        [SerializeField] private Vector3 playerPosition;
        [SerializeField] private bool useCustomPosition;
        [SerializeField] private string sceneName;
        [SerializeField] private PlayableDirector cutscene;
        [SerializeField] private GameObject skipButton;

        [SerializeField] private GameObject playerCanvas;

        private void ChangeActive(bool active)
        {
            if (player != null)
            {
                player.SetActive(!active);
            }

            if (cutsceneCamera != null)
            {
                cutsceneCamera.SetActive(active);
            }

            if (playerCanvas != null)
            {
                playerCanvas.SetActive(!active);
            }

            if (skipButton != null)
            {
                skipButton.SetActive(active);
                var cutsceneButton = skipButton.GetComponent<SkipCutsceneButton>();
                cutsceneButton.Init(cutscene);
                CursorController.Confine();
            }
        }
    
        public void OnCutsceneStart()
        {
            ChangeActive(true);
        }
    
        public void OnCutsceneEnd()
        {
            ChangeActive(false);

            if (useCustomPosition)
            {
                player.transform.position = playerPosition;
            }

            if (!string.IsNullOrEmpty(sceneName))
            {
                LoadingManager.Instance.Load(sceneName);
            }

            if (blackScreen != null)
            {
                Tween.Alpha(blackScreen, 1, 0, 1); 
            }
            
            CursorController.Lock();
        }
    }
}