using UnityEngine;

public static class MoveImplementations
{
    //Damage Move
    public static void ExecuteDamageMove(DamageMove move, CharacterStats attacker, CharacterStats defender, IHealth defenderHealth)
    {
        float damageDealt = (attacker.baseAttack - defender.baseDefense) + move.damage;
        defenderHealth.TakeDamage(damageDealt);
    }
    public static void ExecuteStatMove(StatMove move, CharacterStats target)
    {
        switch (move.stat)
        {
            case StatMove.Stat.Attack:
                target.ChangeBaseAttack(move.statChange);
                break;
            case StatMove.Stat.Defense:
                target.ChangeBaseDefense(move.statChange);
                break;
            case StatMove.Stat.Speed:
                target.ChangeBaseSpeed(move.statChange);
                break;
        }
    }
}