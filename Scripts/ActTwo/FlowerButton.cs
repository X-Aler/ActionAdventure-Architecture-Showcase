using Managers;
using Other;
using UnityEngine;

namespace ActTwo
{
    public class FlowerButton : MonoBehaviour
    {
        [SerializeField] private Flowers needFlower;
        [SerializeField] private DialogueData haveFlowerDialogue;
        [SerializeField] private DialogueData notHaveFlowerDialogue;
        public void OnClick()
        {
            var hasFlower = FlowersManager.Instance.HasFlower(needFlower);
            DialogueManager.Instance.StartDialogue(hasFlower ? haveFlowerDialogue : notHaveFlowerDialogue);
        }
    }
}
