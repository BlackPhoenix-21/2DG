using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public int attack;
    public float criticalChance;
    public GameObject magicBallPrefab;
    public Transform shootPoint;

    public bool isAttacking;

    private Animator animator;
    private bool canAttack = true;
    private float attackCooldown = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            StartAttack();
        }
    }

    void StartAttack()
    {
        isAttacking = true;
        canAttack = false;
        animator.SetTrigger("Attack");

        GameObject MagicBall = Instantiate(magicBallPrefab, shootPoint.position, Quaternion.identity);

        // Определяем направление: вправо или влево
            Vector2 dir = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Передаём направление
        MagicBall.GetComponent<MagicBall>().SetDirection(dir);

        // Сброс через 2 секунды
        Invoke(nameof(ResetAttack), attackCooldown);
    }

    void ResetAttack()
    {
        canAttack = true;
        isAttacking = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isAttacking)
        {
            Attack(collision.gameObject);
        }
    }

    public void Attack(GameObject enemy)
    {
        bool isCritical = Random.value < criticalChance;
        int damage = isCritical ? Mathf.RoundToInt(attack * 2f) : attack;

        Debug.Log(isCritical ? $"Kritischer Treffer! Schaden: {damage}" : $"Normaler Angriff. Schaden: {damage}");

        isAttacking = false;
    }
}
