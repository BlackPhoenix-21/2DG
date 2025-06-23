using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // подключаем Animator
    }

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;

        // Обновляем параметры анимации
        if (animator != null)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("IsMoving", direction != Vector2.zero);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
