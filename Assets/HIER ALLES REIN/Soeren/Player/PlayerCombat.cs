using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attack;
    public float criticalChance;
    public GameObject weapon;
    public bool isAttacking;

    public void Attack(GameObject enemy)
    {
        bool isCritical = Random.value < criticalChance;
        int damage = isCritical ? Mathf.RoundToInt(attack * 2f) : attack;
        Debug.Log(isCritical ? $"Kritischer Treffer! Schaden: {damage}" : $"Normaler Angriff. Schaden: {damage}");
        // Gegner treffen etc.

        isAttacking = false; // Angriff abgeschlossen
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isAttacking)
        {
            Attack(collision.gameObject);
        }
    }
}
