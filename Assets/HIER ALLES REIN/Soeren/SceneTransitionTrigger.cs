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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isTransitioning && other.CompareTag("Player"))
        {
            StartCoroutine(FadeAndSwitchScene());
        }
    }

    private IEnumerator FadeAndSwitchScene()
    {
        isTransitioning = true;

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
}