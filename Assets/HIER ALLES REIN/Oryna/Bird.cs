using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class BirdRandomCorners2D : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;          // базовая скорость
    public Vector2 speedRandom = new Vector2(0.9f, 1.2f); // множитель скорости (рандом)
    public float respawnDelay = 2f;   // пауза между рейсами
    public float margin = 0.08f;      // на сколько вылетать за край

    Camera cam;
    float zDepth;
    SpriteRenderer sr;

    enum Corner { BottomLeft, BottomRight, TopLeft, TopRight }

    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        zDepth = Mathf.Abs(transform.position.z - cam.transform.position.z);

        StartCoroutine(Loop());
    }

    IEnumerator Loop()
    {
        while (true)
        {
            // выбрать разные углы старта и финиша
            Corner from = (Corner)Random.Range(0, 4);
            Corner to   = from;
            while (to == from) to = (Corner)Random.Range(0, 4);

            // позиции чуть за экраном
            Vector3 startPos = CornerToWorld(from, outside:true);
            Vector3 endPos   = CornerToWorld(to,   outside:true);

            // телепорт на старт
            transform.position = startPos;

            // отзеркалить по направлению полета (влево -> flipX = true)
            FaceRight(endPos.x > startPos.x);

            // рандомизируем скорость слегка
            float s = speed * Random.Range(speedRandom.x, speedRandom.y);

            // летим
            while ((endPos - transform.position).sqrMagnitude > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, endPos, s * Time.deltaTime);
                yield return null;
            }

            // ждём и повторяем
            yield return new WaitForSeconds(respawnDelay);
        }
    }

    Vector3 CornerToWorld(Corner c, bool outside)
    {
        // координаты углов во viewport
        float x = (c == Corner.BottomLeft || c == Corner.TopLeft)  ? 0f : 1f;
        float y = (c == Corner.BottomLeft || c == Corner.BottomRight) ? 0f : 1f;

        // чуть за экран
        if (outside)
        {
            x += (x < 0.5f ? -margin : +margin);
            y += (y < 0.5f ? -margin : +margin);
        }

        var w = cam.ViewportToWorldPoint(new Vector3(x, y, zDepth));
        w.z = transform.position.z; // держим наш Z
        return w;
    }

    void FaceRight(bool right)
    {
        if (sr != null) sr.flipX = !right;  // вправо => flipX=false, влево => true
        else
        {
            var s = transform.localScale;
            s.x = Mathf.Abs(s.x) * (right ? 1 : -1);
            transform.localScale = s;
        }
    }
}
