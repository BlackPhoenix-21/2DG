using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;
    public CharacterData playerData;

    public List<GameObject> Level = new();
    public GameObject currentLevel;

    [HideInInspector]
    public bool isTransitioning = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Level.AddRange(GameObject.FindGameObjectsWithTag("Level"));
        Level.Sort((a, b) => a.name.CompareTo(b.name));
        currentLevel = Level[0];
        for (int i = 1; i < Level.Count; i++)
        {
            Level[i].SetActive(false);
        }
        gameObject.AddComponent<Fade>();
    }
}

public class Fade : MonoBehaviour
{
    public string targetSceneName;
    public GameObject spawnPosition;
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 1f;

    public bool isTransitioning = false;
    public IEnumerator FadeAndSwitchScene()
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
        foreach (GameObject level in GameManager.instance.Level)
        {
            if (level == GameManager.instance.currentLevel)
            {
                continue;
            }
            level.SetActive(false);
        }

        GameObject targetLevel = GameManager.instance.Level.Find(x => x.name == targetSceneName);
        if (targetLevel != null)
        {
            targetLevel.SetActive(true);

            // Optional: Spawnposition kann in der neuen Szene genutzt werden
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                player.transform.position = spawnPosition.transform.position;
            }

            // Starte FadeOut nur, wenn dieses GameObject aktiv ist
            if (targetLevel.activeInHierarchy)
            {
                StartCoroutine(FadeOut());
            }
        }
    }

    private IEnumerator FadeOut()
    {
        isTransitioning = false;
        GameManager.instance.isTransitioning = false;
        GameManager.instance.currentLevel.gameObject.SetActive(false);

        // Blende einblenden (Alpha von 1 auf 0)
        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeCanvasGroup.alpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            yield return null;
        }
        fadeCanvasGroup.alpha = 0f;

        GameManager.instance.currentLevel = GameManager.instance.Level.Find(x => x.name == targetSceneName);
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
