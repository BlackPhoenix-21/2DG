using UnityEngine;

public class SceneTransitionTrigger : MonoBehaviour
{
    [Header("Szenenwechsel Einstellungen")]
    public string targetSceneName;
    public GameObject spawnPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fade fade = GameManager.instance.gameObject.GetComponent<Fade>();
        if (!fade.isTransitioning && other.CompareTag("Player"))
        {
            fade.targetSceneName = targetSceneName;
            fade.spawnPosition = spawnPosition;
            fade.fadeCanvasGroup = fadeCanvasGroup;
            fade.fadeDuration = fadeDuration;
            StartCoroutine(fade.FadeAndSwitchScene());
        }
    }
}