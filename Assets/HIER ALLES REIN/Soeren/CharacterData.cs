using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character/Character Data")]
public class CharacterData : ScriptableObject
{
    [Header("Basiswerte")]
    public string characterName;
    public int maxHP = 100;
    public int currentHP = 100;
    public int maxMP = 50;
    public int currentMP = 50;
    public int attack = 10;
    public int defense = 5;
    public int level = 1;
    public int experience = 0;

    [Header("Weitere Werte")]
    public float moveSpeed = 5f;
    public float attackSpeed = 1f;
    public float criticalChance = 0.05f;
}
