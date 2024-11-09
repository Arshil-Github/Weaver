using UnityEngine;

public class Move : ScriptableObject
{
    public string moveName;
    
    public enum Elements
    {
        Fire,
        Water,
        Earth,
        Normal
    }
    public Elements element;
    
    public int powerPoints;
    
    [Range(0, 100)]
    public float accuracy = 100;
}

[CreateAssetMenu(fileName = "New Damage Move", menuName = "Move/Damage Move")]
public class DamageMove : Move
{
    public float damage;
}

[CreateAssetMenu(fileName = "New Status Move", menuName = "Move/Status Move")]
public class StatusMove : Move
{
    public enum Status
    {
        Burn,
        Paralyze,
        Confusion
    }
    public Status status;
    public int forTurns;
}

[CreateAssetMenu(fileName = "New Heal Move", menuName = "Move/Heal Move")]
public class HealMove : Move
{
    public float healAmount;
}

[CreateAssetMenu(fileName = "New Stat Move", menuName = "Move/Stat Move")]
public class StatMove : Move
{
    public enum Stat
    {
        Attack,
        Defense,
        Speed
    }
    public Stat stat;
    public int statChange;
}