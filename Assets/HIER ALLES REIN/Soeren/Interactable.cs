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
    public KeyCode interactionKey = KeyCode.E;

    [Tooltip("Was soll passieren, wenn der Spieler interagiert?")]
    public UnityEvent onInteract;

    private bool isPlayerInRange = false;
    private GameObject player;

    private void Reset()
    {
        Debug.Log($"[Interactable] Reset wird aufgerufen bei {gameObject.name}");
        EnsureMainCollider();
        SetupForInteractionType();
    }

    private void OnValidate()
    {
        // Nur im Editor zur Laufzeit
        SetupForInteractionType();
    }

    private void EnsureMainCollider()
    {
        Collider mainCollider = GetComponent<Collider>();
        if (mainCollider != null)
        {
            mainCollider.isTrigger = false;
        }
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

                BoxCollider triggerCollider = triggerZone.AddComponent<BoxCollider>();
                triggerCollider.isTrigger = true;
                triggerCollider.size = new Vector3(3f, 2f, 3f); // Etwas größer als Hauptobjekt

                Rigidbody rb = triggerZone.AddComponent<Rigidbody>();
                rb.isKinematic = true;
                rb.useGravity = false;

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
