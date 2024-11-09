using System;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    
    [SerializeField] private DialogueUI dialogueUI;

    private List<DialogueData> dialogueQueue = new List<DialogueData>();
    private bool isDialogueActive = false;
    private int currentDialogueIndex = 0;
    private bool isOptionsActive = false;

    public EventHandler OnManagerStartSetup;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void StartInvoke () => OnManagerStartSetup?.Invoke(this, EventArgs.Empty);
    private void Start()
    {
        dialogueUI.gameObject.SetActive(true);
        dialogueUI.HideDialogueBox();
        
        Invoke(nameof(StartInvoke), 0.1f);
    }
    private void Update()
    {
        if (isDialogueActive && Input.GetMouseButtonDown(0) && !isOptionsActive)
        {
            if(dialogueQueue[currentDialogueIndex].options.Count > 0)
            {
                dialogueUI.ShowOptions(dialogueQueue[currentDialogueIndex].options, ShowDialogueByIndex);
                isOptionsActive = true;
            }
            else
            {
                if (currentDialogueIndex >= dialogueQueue.Count - 1 || dialogueQueue[currentDialogueIndex].lastDialogue)
                {
                    CleanUpDialogue();
                }
                else
                {
                    currentDialogueIndex++;
                    ShowDialogueByIndex(currentDialogueIndex);
                }
            }
        }
    }
    public void StartConversation(ConversationTrigger conversationTrigger)
    {
        Player.Instance.SetMovability(false);

        dialogueUI.ShowDialogueBox();

        isDialogueActive = true;

        foreach (DialogueData d in conversationTrigger.GetDialogues())
        {
            dialogueQueue.Add(d);
        }
        
        ShowDialogueByIndex(0);
    }
    private void CleanUpDialogue()
    {
        dialogueQueue.Clear();
        isDialogueActive = false;
        currentDialogueIndex = 0;
        dialogueUI.HideDialogueBox();
        
        //Enable player movement
        Player.Instance.SetMovability(true);
    }

    public void ShowDialogueByIndex(int i)
    {
        currentDialogueIndex = i;
        dialogueUI.SetDialogue(dialogueQueue[i]);
        dialogueQueue[i].OnDialogueEnd?.Invoke();

        isOptionsActive = false;
    }
}