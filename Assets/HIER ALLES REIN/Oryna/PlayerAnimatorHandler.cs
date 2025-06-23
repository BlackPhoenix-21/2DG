using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimatorHandler : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement movement;
    private PlayerCombat combat;

    void Start()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();
    }

    void Update()
    {
        // Обновление параметра скорости
        animator.SetFloat("Speed", movementDirection.magnitude);

        // Запуск атаки
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }
    }

    // Получим направление движения (аналогично тому, что есть в контроллере)
    private Vector3 movementDirection
    {
        get
        {
            Vector3 dir = Vector3.zero;
            if (Input.GetKey(KeyCode.W)) dir = Vector3.forward;
            if (Input.GetKey(KeyCode.S)) dir = Vector3.back;
            if (Input.GetKey(KeyCode.A)) dir = Vector3.left;
            if (Input.GetKey(KeyCode.D)) dir = Vector3.right;
            return dir;
        }
    }
}
