using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class ConversationTrigger : MonoBehaviour
{
    [SerializeField]private List<DialogueData> dialogues;
    
    public virtual List<DialogueData> GetDialogues()
    {
        return dialogues;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.OnActionButtonPressed += (sender, args) =>
            {
                DialogueManager.Instance.StartConversation(this);
            };
            player.ShowTalkPrompt();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.OnActionButtonPressed = null;
        }
    }
}

[Serializable]
public class DialogueData
{
    public enum CharacterEmotions
    {
        Happy,
        Sad,
        Angry,
        Neutral
    } 
    public string name;
    [TextArea(3, 10)] public string dialogueLine;
    
    public CharacterEmotions characterEmotion;
    
    public List<OptionData> options;

    public bool lastDialogue = false;
    
    public UnityEvent OnDialogueEnd;
}

[Serializable]
public class OptionData
{
    [TextArea(3, 10)] public string dialogueLine;
    public int nextDialogueIndex;
}