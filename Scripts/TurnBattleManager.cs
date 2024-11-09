using System;
using System.Collections.Generic;
using UnityEngine;

public class TurnBattleManager : MonoBehaviour
{
    public static TurnBattleManager Instance { get; private set; }
    [SerializeField] private BattleUI battleUI;
    private Player player;
    private BattleNPC enemyNPC;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    private void Start()
    {
        battleUI.gameObject.SetActive(true);
        battleUI.Hide();
    }

    public void StartBattle(Player player, BattleNPC enemyNPC)
    {
        this.player = player;
        this.enemyNPC = enemyNPC;
        
        battleUI.Show();
        //Setup battle UI
        battleUI.SetupBattleUI(player.GetMaxHealth(), enemyNPC.GetMaxHealth());
        
        //Setup player moves
        foreach(Move m in player.GetMoves())
        {
            battleUI.AddPlayerActionMove(m, (move) =>
            {
                currentPlayerMove = move;
                BattleCalculations();
            });
        }
        
        //Set OnDeath Events
        enemyNPC.OnDeath += (sender, args) =>
        {
            battleUI.ChangeToWon(EndBattle);
        };
        player.OnDeath += (sender, args) =>
        {
            battleUI.ChangeToLost(EndBattle);
        };
    }
    //Player Action, Enemy Action

    private Move currentPlayerMove;
    private Move currentEnemyMove;
    public void BattleCalculations()
    {
        bool isPlayerFaster = player.GetCharacterStats().baseSpeed > enemyNPC.GetCharacterStats().baseSpeed;
        currentEnemyMove = enemyNPC.GetBattleMove();
        
        if (isPlayerFaster)
        {
            battleUI.ChangeToPlayerTurn(currentPlayerMove, () =>
            {
                PlayerMove(currentPlayerMove);//To be called from UI
            });
            battleUI.ChangeToEnemyTurn(currentEnemyMove, () =>
            {
                EnemyMove(currentEnemyMove);//To be called from UI
            });
        }
        else
        {
            battleUI.ChangeToEnemyTurn(currentEnemyMove, () =>
            {
                EnemyMove(currentEnemyMove);//To be called from UI
            });
            battleUI.ChangeToPlayerTurn(currentPlayerMove, () =>
            {
                PlayerMove(currentPlayerMove);//To be called from UI
            });
        }
    }
    public void EndBattle()
    {
        battleUI.Hide();
        player.SetMovability(true);
        enemyNPC = null;
    }
    //Function to be called from the move button
    public void PlayerMove(Move move)
    {
        //Check if the move is damageMove or statMove
        switch (move)
        {
            case DamageMove damageMove:
                MoveImplementations.ExecuteDamageMove(damageMove, player.GetCharacterStats(), enemyNPC.GetCharacterStats(), enemyNPC);
                break;
            case StatMove statMove:
                MoveImplementations.ExecuteStatMove(statMove, enemyNPC.GetCharacterStats());
                break;
            default:
                Debug.LogError("Move Not Implemented");
                break;
        }

        if (enemyNPC != null)
        {
            battleUI.UpdateEnemyHealth(enemyNPC.GetCurrentHealth());
        }
    }
    public void EnemyMove(Move move)
    {
        //Check if the move is damageMove or statMove
        switch (move)
        {
            case DamageMove damageMove:
                MoveImplementations.ExecuteDamageMove(damageMove, enemyNPC.GetCharacterStats(), player.GetCharacterStats(), player);
                break;
            case StatMove statMove:
                MoveImplementations.ExecuteStatMove(statMove, player.GetCharacterStats());
                break;
            default:
                Debug.LogError("Move Not Implemented");
                break;
        }
        battleUI.UpdatePlayerHealth(player.GetCurrentHealth());
    }
}