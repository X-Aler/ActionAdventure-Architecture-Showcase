using UnityEngine;

namespace Other
{
    public class GachaButton : MonoBehaviour
    {
        private GameObject gachaCanvas;
        private void Awake()
        {
            var all = FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None);

            foreach (var t in all)
            {
                if (t.name == "Gacha")
                {
                    gachaCanvas = t.gameObject;
                    break;
                }
            }
        }

        public void OnClick()
        {
            gachaCanvas.SetActive(true);
        }
    }
}