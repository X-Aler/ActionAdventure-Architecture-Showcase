using UnityEngine;

namespace Other
{
    public enum ItemsIDs
    {
        KeyOne,
        BerlinKey,
        MosqueKey,
        MainDoorKey,
        Crowbar,
        Hacksaw,
    }
    
    public class InteractData : MonoBehaviour
    {
        public string objectName;
        public InteractActionData interactActionData;
        public ItemsIDs itemID;
        public int showPriority;
        [HideInInspector] public int currentDataIndex;
        public ActionInfo GetCurrentAction()
        {
            return interactActionData.actions[currentDataIndex];
        }

        public void NextAction()
        {
            currentDataIndex++;
            if (currentDataIndex >= interactActionData.actions.Length)
            {
                currentDataIndex = 0;
            }
        }
    }
}
