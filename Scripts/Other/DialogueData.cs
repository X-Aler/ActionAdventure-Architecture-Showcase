using UnityEngine;

namespace Other
{
    [System.Serializable]

    public class AnswerData
    {
        [TextArea(2, 5)]
        public string answerText;
        public int answerIndex = -1;
    }

    [System.Serializable]
    public class DialogueLine
    {
        [TextArea(2, 5)]
        public string dialogueText;
        public bool isAnswer = false;
        public bool isSpecial = false;
        public AnswerData[] answers;
    }
    
    [System.Serializable]
    public class Data
    {
        public Sprite characterSprite;
        public GameObject specialButton;
        public DialogueLine[] dialogueLines;
    }

    [CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptable Objects/DialogueData")]
    public class DialogueData : ScriptableObject
    {
        public Data[] data;
        public bool isOnetime = false;
    }
}
