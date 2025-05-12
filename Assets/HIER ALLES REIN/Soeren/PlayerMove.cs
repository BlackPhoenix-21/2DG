using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    public CharacterData characterData; // Neues Feld f�r CharacterData

    private Vector3 moveDirection;
    private Rigidbody rb;
    private KeyCode lastDirectionKey = KeyCode.None;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (characterData != null)
        {
            moveSpeed = characterData.moveSpeed;
            // Hier k�nnen weitere Werte aus CharacterData �bernommen werden, falls ben�tigt
        }
    }
    void Update()
    {
        HandleInput();
        RotatePlayer(); // Added method call to rotate the player
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void HandleInput()
    {
        // Reihenfolge der Priorit�t: S > W > A > D
        // Wir pr�fen, welche Taste zuletzt gedr�ckt wurde und setzen die Richtung entsprechend.
        // Wir merken uns, welche Taste zuletzt gedr�ckt wurde.
        // Wenn eine Taste losgelassen wird, pr�fen wir, ob noch andere gedr�ckt sind und wechseln ggf. zur�ck.

        // Wir speichern den aktuellen Zustand der Richtungs-Tasten
        bool w = Input.GetKey(KeyCode.W);
        bool s = Input.GetKey(KeyCode.S);
        bool a = Input.GetKey(KeyCode.A);
        bool d = Input.GetKey(KeyCode.D);

        // Wir merken uns, welche Taste zuletzt gedr�ckt wurde
        // Dazu ben�tigen wir ein Feld, das wir au�erhalb der Methode deklarieren:
        // private KeyCode lastDirectionKey = KeyCode.None;
        // F�ge das Feld oben in die Klasse ein, falls noch nicht vorhanden.

        // Taste gedr�ckt?
        if (Input.GetKeyDown(KeyCode.S))
            lastDirectionKey = KeyCode.S;
        else if (Input.GetKeyDown(KeyCode.W))
            lastDirectionKey = KeyCode.W;
        else if (Input.GetKeyDown(KeyCode.A))
            lastDirectionKey = KeyCode.A;
        else if (Input.GetKeyDown(KeyCode.D))
            lastDirectionKey = KeyCode.D;

        // Taste losgelassen?
        if (Input.GetKeyUp(lastDirectionKey))
        {
            // Nach Priorit�t pr�fen, welche Taste noch gehalten wird
            if (s) lastDirectionKey = KeyCode.S;
            else if (w) lastDirectionKey = KeyCode.W;
            else if (a) lastDirectionKey = KeyCode.A;
            else if (d) lastDirectionKey = KeyCode.D;
            else lastDirectionKey = KeyCode.None;
        }

        // Richtung setzen
        switch (lastDirectionKey)
        {
            case KeyCode.S:
                moveDirection = Vector3.back;
                break;
            case KeyCode.W:
                moveDirection = Vector3.forward;
                break;
            case KeyCode.A:
                moveDirection = Vector3.left;
                break;
            case KeyCode.D:
                moveDirection = Vector3.right;
                break;
            default:
                moveDirection = Vector3.zero;
                break;
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            rb.rotation = targetRotation;
        }
    }
}
