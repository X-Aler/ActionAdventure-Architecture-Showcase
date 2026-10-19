using Managers;
using TMPro;
using UnityEngine;

namespace ActOne
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textToShow;
        [SerializeField] private ShowText showText;
        [SerializeField] private float showDuration;
        [SerializeField] private AudioClip actBackgroundSound;
        [SerializeField] private AudioClip actBeforeSound;
    
        public void StartScene()
        {
            var textShowManager = new TextShowManager(textToShow, showDuration);
            StartCoroutine(textShowManager.ShowText(showText));
            
            if (actBackgroundSound)
            {
                AudioManager.Instance.PlayLoop(actBackgroundSound);
            }

            if (actBeforeSound)
            {
                AudioManager.Instance.StopLoop(actBeforeSound);
            }
        }
    }
}
