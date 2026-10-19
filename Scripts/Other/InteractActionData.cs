using UnityEngine;

namespace Other
{
    [System.Serializable]
    public class ActionInfo
    {
        public InteractActions action;
        public string actionName;
        public AudioClip actionSound;
    }

    [CreateAssetMenu(fileName = "InteractActionData", menuName = "Scriptable Objects/Interact Action")]
    public class InteractActionData : ScriptableObject
    {
        public ActionInfo[] actions;
    }
}