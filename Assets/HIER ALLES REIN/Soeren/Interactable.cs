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

public class Interactable : MonoBehaviour
{
    [Header("Interaktions-Einstellungen")]
    public string promptText = "Drücke E, um zu interagieren";
    public InteractionType interactionType = InteractionType.None;
    public KeyCode interactionKey = KeyCode.E;

    [Tooltip("Was soll passieren, wenn der Spieler interagiert?")]
    public UnityEvent onInteract;

    private bool isPlayerInRange = false;
    private GameObject player;

    private void Reset()
    {
        EnsureMainCollider();
        SetupForInteractionType();
    }

    private void OnValidate()
    {
        // Nur im Editor zur Laufzeit
        EnsureMainCollider();
        SetupForInteractionType();
    }

    private void EnsureMainCollider()
    {
        // Prüfe, ob ein 2D-Collider existiert, ansonsten füge einen BoxCollider2D hinzu
        Collider2D mainCollider = GetComponent<Collider2D>();
        if (mainCollider == null)
        {
            mainCollider = gameObject.AddComponent<BoxCollider2D>();
        }
        mainCollider.isTrigger = false;
    }

    private void SetupForInteractionType()
    {
        if (interactionType == InteractionType.Chest)
        {
            Transform existing = transform.Find("InteractionZone");
            if (existing == null)
            {
                GameObject triggerZone = new GameObject("InteractionZone");
                triggerZone.transform.SetParent(transform);
                triggerZone.transform.localPosition = Vector3.zero;

                BoxCollider2D triggerCollider = triggerZone.AddComponent<BoxCollider2D>();
                triggerCollider.isTrigger = true;
                triggerCollider.size = new Vector2(3f, 2f); // Etwas größer als Hauptobjekt

                Rigidbody2D rb = triggerZone.AddComponent<Rigidbody2D>();
                rb.gravityScale = 0;

                InteractionTrigger relay = triggerZone.AddComponent<InteractionTrigger>();
                relay.SetParent(this);
            }
            else
            {
                InteractionTrigger relay = existing.GetComponent<InteractionTrigger>();
                relay.SetParent(this);
            }
        }
    }

    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(interactionKey))
        {
            Interact(player);
        }
    }

    public virtual void Interact(GameObject interactor)
    {
        onInteract?.Invoke();
    }

    public string GetPrompt()
    {
        return promptText;
    }

    public void PlayerEntered(GameObject go)
    {
        isPlayerInRange = true;
        player = go;
        if (UIManager.Instance != null)
            UIManager.Instance.ShowPrompt(promptText);
    }

    public void PlayerExited()
    {
        isPlayerInRange = false;
        player = null;
        if (UIManager.Instance != null)
            UIManager.Instance.HidePrompt();
    }
}
