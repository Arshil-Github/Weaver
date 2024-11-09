using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private ConversationTrigger levelStartConversation;
    
    private void Start()
    {
        if(levelStartConversation.GetDialogues().Count > 0)
            DialogueManager.Instance.OnManagerStartSetup += (sender, args) =>
            {
                TriggerInitialDialogue();
            };
    }

    public void TriggerInitialDialogue()
    {
        DialogueManager.Instance.StartConversation(levelStartConversation);
    }
}
