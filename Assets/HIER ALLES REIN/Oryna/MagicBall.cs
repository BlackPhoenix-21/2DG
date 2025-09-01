using UnityEngine;

public class MagicBall : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.right;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // отражаем спрайт, если летим влево
        if (direction.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            // тут можно нанести урон врагу
            Destroy(gameObject); // уничтожаем шар при столкновении
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f); // исчезает через 5 секунд, если никуда не попал
    }
}
