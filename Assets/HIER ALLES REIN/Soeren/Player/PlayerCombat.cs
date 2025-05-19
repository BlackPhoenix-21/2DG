using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attack;
    public float criticalChance;

    public void Attack()
    {
        bool isCritical = Random.value < criticalChance;
        int damage = isCritical ? Mathf.RoundToInt(attack * 2f) : attack;
        Debug.Log(isCritical ? $"Kritischer Treffer! Schaden: {damage}" : $"Normaler Angriff. Schaden: {damage}");
        // Gegner treffen etc.
    }
}
