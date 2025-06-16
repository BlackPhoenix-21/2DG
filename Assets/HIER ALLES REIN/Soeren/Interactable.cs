using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    None,
    Chest,
    Door,
    NPC,
    Pickup
}

[RequireComponent(typeof(Collider))]
public class Interactable : MonoBehaviour
{
    [Header("Interaktions-Einstellungen")]
    public string promptText = "Drücke E, um zu interagieren";
    public InteractionType interactionType = InteractionType.None;

    [Tooltip("Was soll passieren, wenn der Spieler interagiert?")]
    public UnityEvent onInteract;

    private void Reset()
    {
        // Automatisch Collider auf Trigger setzen
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    /// <summary>
    /// Wird vom PlayerInteraction-Script aufgerufen, wenn der Spieler interagiert.
    /// </summary>
    public void Interact(GameObject interactor)
    {
        Debug.Log($"{gameObject.name} wurde interagiert. Typ: {interactionType}");
        onInteract?.Invoke();
    }

    /// <summary>
    /// Gibt den Text zurück, der im UI angezeigt werden soll.
    /// </summary>
    public string GetPrompt()
    {
        return promptText;
    }
}
