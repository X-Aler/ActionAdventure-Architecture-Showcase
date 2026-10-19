using System;
using Managers;
using Other;
using UnityEngine;

public class BootsTrap : MonoBehaviour
{
    [SerializeField] private DialogueManager  dialogueManager;
    [SerializeField] private DialogueData dialogueData;

    private void Start()
    {
        dialogueManager.StartDialogue(dialogueData);
    }
}
