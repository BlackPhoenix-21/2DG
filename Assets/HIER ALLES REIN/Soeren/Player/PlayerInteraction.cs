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
        Debug.Log($"Ray origin: {ray.origin}, direction: {ray.direction}");
        Debug.DrawRay(ray.origin, ray.direction * interactionRange, Color.green, 0.1f);
        if (Physics.Raycast(ray, out RaycastHit hit, interactionRange, interactableLayer))
        {
            Debug.Log($"Raycast hit: {hit.collider.name} at distance {hit.distance}");
            currentInteractable = hit.collider.GetComponent<Interactable>();

            if (currentInteractable != null)
            {
                // Optional: Zeige Prompt im UI
                if (UIManager.Instance != null)
                    UIManager.Instance.ShowPrompt(currentInteractable.GetPrompt());
                return;
            }
        }

        if (UIManager.Instance != null)
            UIManager.Instance.HidePrompt();
    }
}
