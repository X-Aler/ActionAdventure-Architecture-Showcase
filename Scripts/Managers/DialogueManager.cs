using System;
using System.Collections;
using Other;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueCanvas;
        [SerializeField] private GameObject playerCamera;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject skipButton;
        [SerializeField] private GameObject dialoguePanel;
        
        [SerializeField] private TextMeshProUGUI dialogueText;
        [SerializeField] private Image dialogueCharacter;
        [SerializeField] private Button answerButton;
        
        [SerializeField] private AudioClip writeSound;
        
        [SerializeField] private float printSpeed = 0.1f;

        public static DialogueManager Instance;
        
        private int _textIndex;
        private int _currentDialogueIndex;
        private bool _textShowing;
        private bool _answered;
        
        private Coroutine _printCoroutine;
        
        private DialogueLine[] _currentText;
        private DialogueData _dialogueData;

        private void Awake()
        {
            if (Instance && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public void StartDialogue(DialogueData dialogueData)
        {
            CursorController.Unlock();
            dialogueCanvas.SetActive(true);
            playerCamera.SetActive(false);
            player.SetActive(false);
            skipButton.SetActive(true);
            _answered = false;

            _dialogueData = dialogueData;
            _currentDialogueIndex = 0;
            ShowNextCharacterDialogue();
        }

        private void ShowNextCharacterDialogue()
        {
            if (_currentDialogueIndex >= _dialogueData.data.Length)
            {
                EndDialogue();
                return;
            }

            var data = _dialogueData.data[_currentDialogueIndex];
            dialogueCharacter.sprite = data.characterSprite;
            _currentText = data.dialogueLines;
            
            _textIndex = 0;
            StartText();
        }
        
        private void StartText()
        {
            if (_printCoroutine != null)
            {
                StopCoroutine(_printCoroutine);
            }
            
            if (_answered)
            {
                _answered = false; 

                while (_textIndex < _currentText.Length && _currentText[_textIndex].isAnswer)
                {
                    _textIndex++;
                }
            }

            if (_textIndex >= _currentText.Length)
            {
                _currentDialogueIndex++;
                ShowNextCharacterDialogue();
                return;
            }
            
            _printCoroutine = StartCoroutine(PrintText(_currentText[_textIndex]));
        }
        
        private IEnumerator PrintText(DialogueLine line)
        {
            dialogueText.text = "";
            _textShowing = true;
            foreach (var c in line.dialogueText)
            {
                dialogueText.text += c;
                if(writeSound) AudioManager.Instance.PlayOneShot(writeSound);
                yield return new WaitForSeconds(printSpeed);
            }

            _textShowing = false;
            
            CheckAnswers(line.answers);
        }
        
        private void CheckAnswers(AnswerData[] answers)
        {
            if (answers != null && answers.Length > 0)
            {
                ShowAnswers(answers);
            }
            else
            {
                _textIndex++;
                StartText();
            }
        }

        private void ShowAnswers(AnswerData[] answers)
        {
            skipButton.SetActive(false);
            ClearAnswerButtons();

            if (_currentText[_textIndex].isSpecial)
            {
                var specialButton = Instantiate(_dialogueData.data[_currentDialogueIndex].specialButton, dialoguePanel.transform);
                specialButton.SetActive(true);
            }
            

            foreach (var answer in answers)
            {
                var button = Instantiate(answerButton, dialoguePanel.transform);
                button.GetComponentInChildren<TextMeshProUGUI>().text = answer.answerText;

                button.onClick.AddListener(() =>
                {
                    skipButton.SetActive(true);
                    ClearAnswerButtons();

                    if (answer.answerIndex >= 0)
                    {
                        _textIndex = answer.answerIndex;
                        
                        _answered = false;

                        StartText();
                        
                        _answered = true;
                    }
                    else
                    {
                        EndDialogue();
                    }
                });
            }
            
        }
        
        private void ClearAnswerButtons()
        {
            foreach (Transform c in dialoguePanel.transform)
            {
                if (c.GetComponent<Button>())
                {
                    Destroy(c.gameObject);
                }
            }
        }

        
        public void SkipText()
        {
            if (_textShowing) 
            {
                if (_printCoroutine != null)
                {
                    StopCoroutine(_printCoroutine);
                }

                _textShowing = false;
                
                var currentLine = _currentText[_textIndex];
                dialogueText.text = currentLine.dialogueText;
                
                CheckAnswers(currentLine.answers);
            }
            else 
            {
                StartText();
            }
        }

        public void EndDialogue()
        {
            CursorController.Lock();
            dialogueCanvas.SetActive(false);
            playerCamera.SetActive(true);
            player.SetActive(true);
            dialogueText.text = "";
            if(dialogueCharacter) dialogueCharacter.sprite = null;
            _textIndex = 0;
            _currentDialogueIndex = 0;
            ClearAnswerButtons();
        }
    }
}