using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Szenenwechsel Einstellungen")]
    public string targetSceneName;
    public GameObject spawnPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private bool isTransitioning = false;

    private void Start()
    {
        if (GameManager.instance.isTransitioning)
        {
            StartCoroutine(FadeOut());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Trigger entered: " + other.name);
        if (!isTransitioning && other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndSwitchScene());
        }
    }

    private IEnumerator FadeAndSwitchScene()
    {
        isTransitioning = true;
        GameManager.instance.isTransitioning = true;

        // Blende einblenden (Alpha von 0 auf 1)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 1f;

        // Szene wechseln
        SceneManager.LoadScene(targetSceneName);
        // Optional: Spawnposition kann in der neuen Szene genutzt werden
    }

    private IEnumerator FadeOut()
    {
        isTransitioning = false;
        GameManager.instance.isTransitioning = false;

        // Blende einblenden (Alpha von 1 auf 0)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;
    }
}