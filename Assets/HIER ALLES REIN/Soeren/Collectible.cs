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
        // F�ge hier weitere Typen hinzu, wenn du sie brauchst
    }

    [Header("Collectible Einstellungen")]
    public CollectibleType type; // Welcher Typ ist dieses Collectible?
    public float value = 1f;    // Der Wert des Collectibles (z.B. 10 f�r Geld, 25 f�r Gesundheit)

    private void OnValidate()
    {
        if (GetComponent<Collider2D>() == null)
        {
            // F�ge einen Collider hinzu, wenn keiner vorhanden ist
            gameObject.AddComponent<BoxCollider2D>();
            gameObject.GetComponent<Collider2D>().isTrigger = true; // Setze den Collider als Trigger
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        // �berpr�fen, ob das Objekt, das kollidiert ist, der Spieler ist
        // Du k�nntest hier auch ein Tag wie "Player" verwenden oder eine spezifische Komponente abfragen
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
                // Beispiel: Spieler-Geld erh�hen
                // player.GetComponent<PlayerWallet>().AddMoney(value);
                break;
            case CollectibleType.Health:
                Debug.Log($"Gesundheit eingesammelt! Wert: {value}");
                player.GetComponent<PlayerController>().characterData.baseStats.maxHP += (int)value; // Beispiel: Spieler-Gesundheit erh�hen
                break;
            case CollectibleType.Score:
                Debug.Log($"Punkte eingesammelt! Wert: {value}");
                // Beispiel: Spieler-Punkte erh�hen
                // player.GetComponent<GameManager>().AddScore(value);
                break;
            case CollectibleType.PowerUp:
                Debug.Log($"Power-Up eingesammelt! Typ: {value}");
                // Beispiel: Ein tempor�res Power-Up aktivieren
                // player.GetComponent<PlayerPowerUpManager>().ActivatePowerUp((int)value); // Wenn value den Power-Up-Typ darstellt
                break;
            default:
                Debug.LogWarning($"Unbekannter CollectibleType: {type}");
                break;
        }
    }

    private void Collect()
    {
        // Zerst�re das Collectible-Objekt nach dem Einsammeln
        Destroy(gameObject);
    }
}