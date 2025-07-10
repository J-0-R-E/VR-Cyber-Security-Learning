using UnityEngine;

public class PaperSpawner : MonoBehaviour
{
    public GameObject paperPrefab;  // Assign your paper prefab in the Inspector
    public Transform spawnPoint;    // Set a location for the paper to appear

    public void SpawnPaper()
    {
        Debug.Log("SpawnPaper() called");  // Debugging log

        if (paperPrefab != null && spawnPoint != null)
        {
            Instantiate(paperPrefab, spawnPoint.position, Quaternion.identity);
            Debug.Log("Paper successfully spawned!");
        }
        else
        {
            Debug.LogError("PaperPrefab or SpawnPoint is missing in PaperSpawner!");
        }
    }

}
