using UnityEngine;

public class InteractionTrigger : MonoBehaviour
{
    private Interactable parent;

    public void SetParent(Interactable interactable)
    {
        parent = interactable;
        Debug.Log(parent);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if (other.CompareTag("Player") && parent != null)
        {
            parent.PlayerEntered(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit");
        if (other.CompareTag("Player") && parent != null)
        {
            parent.PlayerExited();
        }
    }
}
