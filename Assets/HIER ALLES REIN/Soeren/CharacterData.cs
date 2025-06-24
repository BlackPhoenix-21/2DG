using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{
    public string characterName;
    public BaseStats baseStats;
    public MovementStats movementStats;
    public CombatStats combatStats;


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
}

[System.Serializable]
public class MovementStats
{
    public float moveSpeedInt = 5f;
}

