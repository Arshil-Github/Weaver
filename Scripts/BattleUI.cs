using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private CharacterBattleUI playerBattleUI;
    [SerializeField] private CharacterBattleUI enemyBattleUI;
    
    [SerializeField] private TextMeshProUGUI dialogueLines;
    [SerializeField] private GameObject playerMovesParent;
    
    [SerializeField] private Transform actionMoveSinglepf;

    private List<ActionMoveSingleUI> spawnedActionMoveUI;
    
    
    bool inDialogue = false;

    public class DialogueWithAction
    {
        public string dialogue = "";
        public Action AfterAction = null;
    }
    private List<DialogueWithAction> dialogueQueue = new List<DialogueWithAction>();


    private void Awake()
    {
        dialogueQueue = new List<DialogueWithAction>();
        spawnedActionMoveUI = new List<ActionMoveSingleUI>();

    }

    
    public void SetupBattleUI(float playerMaxHealth, float enemyMaxHealth)
    {
        
        playerBattleUI.SetupMaxHealth(playerMaxHealth);
        enemyBattleUI.SetupMaxHealth(enemyMaxHealth);

        spawnedActionMoveUI = new List<ActionMoveSingleUI>();
        dialogueQueue.Clear();
    }

    public void ChangeToPlayerChooseMove()
    {
        inDialogue = false;
        ShowDialogueBox(false);
    }
    public void ChangeToPlayerTurn(Move usedMove, Action afterDialogue)
    {
        inDialogue = true;
        
        dialogueQueue.Add(new DialogueWithAction {dialogue = "Player used " + usedMove.moveName});

        switch (usedMove)
        {
            case DamageMove damageMove:
                dialogueQueue.Add(new DialogueWithAction
                {
                    dialogue = "It hits the enemy!", AfterAction = afterDialogue
                });
                break;
            case StatMove statMove:
                dialogueQueue.Add(new DialogueWithAction {
                    dialogue = "Lowered " + statMove.stat + " by " + statMove.statChange + "!", 
                    AfterAction = afterDialogue
                    
                });
                break;
            default:
                Debug.LogError("Move Not Implemented");
                break;
        }
        
        ChangeToNextDialogue();
        ShowDialogueBox(true);
    }
    public void ChangeToEnemyTurn(Move usedMove, Action afterDialogue)
    {
        inDialogue = true;
        
        dialogueQueue.Add(new DialogueWithAction {dialogue = "Enemy used " + usedMove.moveName});
        
        switch (usedMove)
        {
            case DamageMove damageMove:
                dialogueQueue.Add(new DialogueWithAction {dialogue = "It hits the player!", AfterAction = afterDialogue});
                break;
            case StatMove statMove:
                dialogueQueue.Add(new DialogueWithAction {dialogue = "Increased " + statMove.stat + " by " + statMove.statChange, AfterAction = afterDialogue});
                break;
            default:
                Debug.LogError("Move Not Implemented");
                break;
        }
        
        ShowDialogueBox(true);
    }
    public void ChangeToWon(Action after)
    {
        inDialogue = true;
        
        dialogueQueue.Add(new DialogueWithAction
            {dialogue = "You Won!", AfterAction = after});
        
        ShowDialogueBox(true);
    }
    public void ChangeToLost(Action after)
    {
        inDialogue = true;

        dialogueQueue.Add(new DialogueWithAction
        {
            dialogue = "You Lost!", AfterAction = after
        });
        
        ShowDialogueBox(true);
    }

    private void Update()
    {
        if(inDialogue == true && Input.GetKeyDown(KeyCode.Space))
        {
            ChangeToNextDialogue();
        }
    }

    public void ImidiateSwitchToDialogue(DialogueWithAction d)
    {
        dialogueLines.text = d.dialogue;
        
        if (d.AfterAction != null)
            d.AfterAction();
        
        ShowDialogueBox(true);
    }
    
    public void ChangeToNextDialogue()
    {
        if(dialogueQueue.Count > 0 )
        {
            if (dialogueQueue[0].AfterAction != null)
                dialogueQueue[0].AfterAction();
            
            dialogueQueue.RemoveAt(0);
            
            if (dialogueQueue.Count > 0)
            {
                dialogueLines.text = dialogueQueue[0].dialogue;
            }
            else
            {
                ShowDialogueBox(false);
            }

        }
        else
        {
            ChangeToPlayerChooseMove();
        }
    }


    public void ShowDialogueBox(bool show)
    {
        dialogueLines.gameObject.SetActive(show);
        playerMovesParent.SetActive(!show);
    }
    public void UpdatePlayerHealth(float currentHealth)
    {
        playerBattleUI.SetCurrentHealth(currentHealth);
    }
    public void UpdateEnemyHealth(float currentHealth)
    {
        enemyBattleUI.SetCurrentHealth(currentHealth);
    }
    
    public void AddPlayerActionMove(Move m, Action<Move> moveAction)
    {
        Transform actionMoveUI = Instantiate(actionMoveSinglepf, playerMovesParent.transform);
        actionMoveUI.GetComponent<ActionMoveSingleUI>().Setup(m, moveAction);
        spawnedActionMoveUI.Add(actionMoveUI.GetComponent<ActionMoveSingleUI>());
    }

    public void Hide()
    {
        foreach (ActionMoveSingleUI ui in spawnedActionMoveUI)
        {
            Destroy(ui.gameObject);
        }
        gameObject.SetActive(false);
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
}
