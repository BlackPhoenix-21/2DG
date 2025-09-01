using UnityEngine;

public class Button : MonoBehaviour
{
    public void ActionButton(int actionIndex)
    {
        switch (actionIndex)
        {
            case 0:
                GameManager.instance.menuActiv = false;
                break;
            case 1:
                break;
            case 2:
                Application.Quit();
                break;
            default:
                Debug.Log("Falscher Index für die Buttons");
                break;
        }
    }
}
