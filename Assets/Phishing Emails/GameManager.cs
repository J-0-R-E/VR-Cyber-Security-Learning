using UnityEngine;
using TMPro; // Import TextMeshPro

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PaperSpawner paperSpawner;
    public TextMeshPro scoreText3D; // 3D TextMeshPro for world-space score
    private int score = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Debug.Log("GameManager Start() called! Spawning the first paper.");
        SpawnNewPaper();
        UpdateScoreUI(); // Display initial score
    }

    public void AddScore(int points)
    {
        score += points;
        Debug.Log("Score Updated: " + score);
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText3D != null)
        {
            scoreText3D.text = "Score: " + score;
        }
        else
        {
            Debug.LogError("ScoreText3D UI is not assigned in GameManager!");
        }
    }

    public void SpawnNewPaper()
    {
        Debug.Log("Spawning new paper...");

        if (paperSpawner != null)
        {
            paperSpawner.SpawnPaper();
            Debug.Log("New paper spawned successfully.");
        }
        else
        {
            Debug.LogError("PaperSpawner is not assigned in GameManager!");
        }
    }
}
