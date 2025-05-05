using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Vector3 moveDirection;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            moveDirection = Vector3.back;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveDirection = Vector3.right;
        }
        else
        {
            moveDirection = Vector3.zero;
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
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, Time.deltaTime * moveSpeed);
        }
    }
}
