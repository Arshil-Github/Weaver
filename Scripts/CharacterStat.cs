using System;
using UnityEngine;

[Serializable]
public class CharacterStats
{
    public float baseAttack;
    public float baseDefense;
    public float baseHealth;
    public float baseSpeed;
    
    public void ChangeBaseAttack(float value)
    {
        baseAttack += value;
    }
    public void ChangeBaseDefense(float value)
    {
        baseDefense += value;
    }
    public void ChangeBaseHealth(float value)
    {
        baseHealth += value;
    }
    public void ChangeBaseSpeed(float value)
    {
        baseSpeed += value;
    }
}