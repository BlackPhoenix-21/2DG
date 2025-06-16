using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetDirection(Vector3 direction)
    {
        moveDirection = direction;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
        RotatePlayer();
    }

    private void RotatePlayer()
    {
        if (moveDirection != Vector3.zero)
        {
            rb.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
