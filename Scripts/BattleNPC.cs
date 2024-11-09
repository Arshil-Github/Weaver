using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleNPC : MonoBehaviour, IHealth
{
    [SerializeField] private string npcName;
    [SerializeField] private int level;
    [SerializeField] private CharacterStats stats;
    
    [SerializeField] private List<Move> moves;
    public EventHandler OnHealthChanged { get; set; }
    public EventHandler OnDeath { get; set; }
    private float currentHealth;
    private bool inBattle = false;

    private void Awake()
    {
        currentHealth = stats.baseHealth;
    }

    public Move GetBattleMove()
    {
        return moves[UnityEngine.Random.Range(0, moves.Count)];
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player))
        {
            TurnBattleManager.Instance.StartBattle(player, this);
            player.SetMovability(false);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float healAmount)
    {
        currentHealth += healAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, stats.baseHealth);
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetMaxHealth(float maxHealth)
    {
        stats.baseHealth = maxHealth;
    }

    public float GetMaxHealth()
    {
        return stats.baseHealth;
    }

    public float GetCurrentHealth()
    {
        return currentHealth;
    }
    
    public void Die()
    {
        OnDeath?.Invoke(this, EventArgs.Empty);
        
        Destroy(gameObject);
    }
    
    public CharacterStats GetCharacterStats()
    {
        return stats;
    }
}