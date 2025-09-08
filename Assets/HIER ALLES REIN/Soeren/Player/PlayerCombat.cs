using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerCombat : MonoBehaviour
{
    public int attack;
    public float criticalChance;
    public GameObject magicBallPrefab;
    public Transform shootPoint;
    private PlayerController playerController;

    public GameObject attackArea;
    public bool isAttacking = false;

    private Animator animator;
    private bool canAttack = true;
    private float attackCooldown = 2f;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerController = GetComponent<PlayerController>();
        attackArea.GetComponent<Collider2D>().enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
        {
            //StartAttack();
            Meele();
        }
    }

    void Meele()
    {
        canAttack = false;
        isAttacking = true;
        Vector2 dir = playerController.lookAt;
        if (dir != Vector2.zero)
        {
            switch (dir)
            {
                case Vector2 v when v.x > 0:
                    attackArea.transform.position = new Vector2(transform.position.x + 1, transform.position.y);
                    break;
                case Vector2 v when v.x < 0:
                    attackArea.transform.position = new Vector2(transform.position.x - 1, transform.position.y);
                    break;
                case Vector2 v when v.y > 0:
                    attackArea.transform.position = new Vector2(transform.position.x, transform.position.y + 1);
                    break;
                case Vector2 v when v.y < 0:
                    attackArea.transform.position = new Vector2(transform.position.x, transform.position.y - 1);
                    break;
            }
            attackArea.GetComponent<BoxCollider2D>().enabled = true;
            AttackCollistion();
            attackArea.GetComponent<BoxCollider2D>().enabled = false;
        }
        ResetAttack();
    }

    private void AttackCollistion()
    {
        Debug.Log("AttackCollistion");
        List<Collider2D> colliders = new();
        attackArea.GetComponent<BoxCollider2D>().GetContacts(new ContactFilter2D().NoFilter(), colliders);
        List<GameObject> hits = colliders.ConvertAll(c => c.gameObject);
        foreach (var hit in hits)
        {
            Debug.Log("Hit: " + hit.name);
            if (!hit.CompareTag("Player"))
            {
                try { Attack(hit); }
                catch { Debug.Log("Kein Enemy getroffen"); }
            }
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
        canAttack = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && isAttacking)
        {
            //Attack(collision.gameObject);
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
