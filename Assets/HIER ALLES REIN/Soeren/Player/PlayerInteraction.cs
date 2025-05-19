using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaktions-Einstellungen")]
    public float interactionRange = 2f;
    public KeyCode interactionKey = KeyCode.E;

    [Tooltip("Layer, auf dem sich interaktive Objekte befinden.")]
    public LayerMask interactableLayer;

    private Camera mainCamera;
    private Interactable currentInteractable;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        DetectInteractable();

        if (currentInteractable != null && Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact(gameObject);
        }
    }

    private void DetectInteractable()
    {
        currentInteractable = null;

        Ray ray = new Ray(transform.position + Vector3.up * 0.5f, transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            currentInteractable = hit.collider.GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                // Optional: Zeige Prompt im UI
                UIManager.Instance?.ShowPrompt(currentInteractable.GetPrompt());
                return;
            }
        }

        UIManager.Instance?.HidePrompt();
    }
}
