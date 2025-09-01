using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public Vector2 moveDirection;
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // подключаем Animator
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public bool isBlocked = false;

    public void SetDirection(Vector2 direction)
    {
        moveDirection = direction;
        if (isBlocked) return;
        // Обновляем параметры анимации
        if (animator != null)
        {
            animator.SetFloat("MoveX", direction.x);
            animator.SetFloat("MoveY", direction.y);
            animator.SetBool("IsMoving", direction != Vector2.zero);
            // Flip по горизонтали для левого направления
            if (direction.x != 0)
            {
                spriteRenderer.flipX = direction.x < 0;
            }

        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveDirection * moveSpeed * Time.fixedDeltaTime);
    }
}
