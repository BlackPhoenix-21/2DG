using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameState currentGameState;
    public CharacterData playerData;

    public List<GameObject> Level = new();

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
        for (int i = 1; i < Level.Count; i++)
        {
            Level[i].SetActive(false);
        }
    }
}

public enum GameState
{
    MainMenu,
    Playing,
    Paused,
    GameOver
}
