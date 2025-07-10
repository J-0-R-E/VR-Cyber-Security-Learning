using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [Header("Prefab and Settings")]
    [SerializeField] private GameObject charBlockPrefab;
    [SerializeField] private int numberOfCubesToSpawn = 10;

    [Header("Spawn Area")]
    [SerializeField] private Vector3 center = Vector3.zero;
    [SerializeField] private Vector3 size = new Vector3(5f, 0.5f, 5f);

    [Header("Characters to Use")]
    [SerializeField] private string possibleCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$%";

    private void Start()
    {
        SpawnCubes();
    }

    private void SpawnCubes()
    {
        // If no prefab is assigned, exit early
        if (charBlockPrefab == null)
        {
            Debug.LogError("No CharBlock prefab assigned to CubeSpawner!");
            return;
        }

        for (int i = 0; i < numberOfCubesToSpawn; i++)
        {
            // Calculate a random position within the specified area
            Vector3 randomPos = GetRandomPositionInArea();

            // Instantiate the prefab
            GameObject newCube = Instantiate(charBlockPrefab, randomPos, Quaternion.identity);

            // Optionally, choose a random character from the possibleCharacters string
            char randomChar = possibleCharacters[Random.Range(0, possibleCharacters.Length)];

            // If the prefab has a CharBlock script, assign the chosen character
            CharBlock charBlock = newCube.GetComponent<CharBlock>();
            if (charBlock != null)
            {
                // For demonstration, we’ll also set the character field in code
                // This presumes your CharBlock script has a public setter or a method to set the character
                // If you have a serialized field instead, you can also do charBlock.character = randomChar;
                // Make sure your script allows runtime assignment (public setter or method).
                charBlock.SetCharacter(randomChar);
            }
        }
    }

    private Vector3 GetRandomPositionInArea()
    {
        // Generate random offsets within the 'size' boundaries
        float offsetX = Random.Range(-size.x / 2, size.x / 2);
        float offsetY = Random.Range(-size.y / 2, size.y / 2);
        float offsetZ = Random.Range(-size.z / 2, size.z / 2);

        // Add them to 'center' to get the final position
        Vector3 spawnPos = center + new Vector3(offsetX, offsetY, offsetZ);

        return spawnPos;
    }

    // This OnDrawGizmosSelected method is optional. It helps you visualize the spawn area in the Editor.
    [SerializeField] private Transform spawnCenter;
    [SerializeField] private Vector3 gsize = new Vector3(5f, 5f, 5f);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        if (spawnCenter != null)
        {
            Gizmos.DrawWireCube(spawnCenter.position, gsize);
        }
    }

}
