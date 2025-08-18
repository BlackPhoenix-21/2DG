using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        // Отразим спрайт, если идём влево
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Тут можно добавить урон врагу
            Destroy(gameObject);
        }

        if (!other.CompareTag("Player")) // не исчезает от игрока
        {
            Destroy(gameObject, 0.1f);
             return;
        }
    }

    void Start()
    {
        Destroy(gameObject, 3f); // исчезает через 3 секунды, если не столкнулся
    }
}
