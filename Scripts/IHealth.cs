using System;
using UnityEngine;

public interface IHealth
{
    public void TakeDamage(float damage);
    public void Heal(float healAmount);
    
    public void SetMaxHealth(float maxHealth);
    
    public float GetMaxHealth();
    public float GetCurrentHealth();
    
    public void Die();
}