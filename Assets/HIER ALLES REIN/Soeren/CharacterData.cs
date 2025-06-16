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
    public int maxHP;
    public int maxMP;
    public int level;
}

[System.Serializable]
public class CombatStats
{
    public int attack;
    public int defense;
    public float criticalChance;
    public float attackSpeed;
}

[System.Serializable]
public class MovementStats
{
    public float moveSpeed = 5f;
}

