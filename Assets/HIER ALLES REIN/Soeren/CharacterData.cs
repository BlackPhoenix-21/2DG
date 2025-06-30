using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public BaseStats baseStats;
    public MovementStats movementStats;
    public CombatStats combatStats;

    public void RestStats()
    {
        baseStats.maxHP = baseStats.maxHPInt;
        baseStats.currentHP = baseStats.maxHP;
        baseStats.maxMP = baseStats.maxMPInt;
        baseStats.currentMP = baseStats.maxMP;
        baseStats.level = 1;
        combatStats.attack = combatStats.attackInt;
        combatStats.defense = combatStats.defenseInt;
        combatStats.criticalChance = combatStats.criticalChanceInt;
        combatStats.attackSpeed = combatStats.attackSpeedInt;
        movementStats.moveSpeed = movementStats.moveSpeedInt;
    }
}

[System.Serializable]
public class BaseStats
{
    public int maxHPInt;
    public int maxMPInt;

    [HideInInspector]
    public int maxHP, currentHP, maxMP, currentMP, level;
}

[System.Serializable]
public class CombatStats
{
    public int attackInt;
    public int defenseInt;
    public float criticalChanceInt;
    public float attackSpeedInt;

    [HideInInspector]
    public int attack, defense;
    [HideInInspector]
    public float criticalChance, attackSpeed;
}

[System.Serializable]
public class MovementStats
{
    public float moveSpeedInt = 5f;

    [HideInInspector]
    public float moveSpeed;
}

