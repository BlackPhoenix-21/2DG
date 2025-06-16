using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject promptPanel;
    public TMPro.TextMeshProUGUI promptText;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowPrompt(string message)
    {
        promptPanel.SetActive(true);
        promptText.text = message;
    }

    public void HidePrompt()
    {
        promptPanel.SetActive(false);
    }
}
