using UnityEngine;

public class Collectible : MonoBehaviour
{
    // Enum zur Definition der verschiedenen Arten von Collectibles
    public enum CollectibleType
    {
        Money,
        Health,
        Score,
        PowerUp
        // Füge hier weitere Typen hinzu, wenn du sie brauchst
    }

    [Header("Collectible Einstellungen")]
    public CollectibleType type; // Welcher Typ ist dieses Collectible?
    public float value = 1f;    // Der Wert des Collectibles (z.B. 10 für Geld, 25 für Gesundheit)

    private void OnValidate()
    {
        if (GetComponent<Collider2D>() == null)
        {
            // Füge einen Collider hinzu, wenn keiner vorhanden ist
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.GetComponent<Collider2D>().isTrigger = true; // Setze den Collider als Trigger
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // Überprüfen, ob das Objekt, das kollidiert ist, der Spieler ist
        // Du könntest hier auch ein Tag wie "Player" verwenden oder eine spezifische Komponente abfragen
        if (other.CompareTag("Player"))
        {
            ApplyEffect(other.gameObject);
            Collect();
        }
    }

    private void ApplyEffect(GameObject player)
    {
        // Hier wenden wir den Effekt basierend auf dem CollectibleType an
        switch (type)
        {
            case CollectibleType.Money:
                Debug.Log($"Geld eingesammelt! Wert: {value}");
                // Beispiel: Spieler-Geld erhöhen
                // player.GetComponent<PlayerWallet>().AddMoney(value);
                break;
            case CollectibleType.Health:
                Debug.Log($"Gesundheit eingesammelt! Wert: {value}");
                player.GetComponent<PlayerController>().characterData.baseStats.maxHP += (int)value; // Beispiel: Spieler-Gesundheit erhöhen
                break;
            case CollectibleType.Score:
                Debug.Log($"Punkte eingesammelt! Wert: {value}");
                // Beispiel: Spieler-Punkte erhöhen
                // player.GetComponent<GameManager>().AddScore(value);
                break;
            case CollectibleType.PowerUp:
                Debug.Log($"Power-Up eingesammelt! Typ: {value}");
                // Beispiel: Ein temporäres Power-Up aktivieren
                // player.GetComponent<PlayerPowerUpManager>().ActivatePowerUp((int)value); // Wenn value den Power-Up-Typ darstellt
                break;
            default:
                Debug.LogWarning($"Unbekannter CollectibleType: {type}");
                break;
        }
    }

    private void Collect()
    {
        // Zerstöre das Collectible-Objekt nach dem Einsammeln
        Destroy(gameObject);
    }
}