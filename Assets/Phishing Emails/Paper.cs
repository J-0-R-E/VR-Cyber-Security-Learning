using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Paper : MonoBehaviour
{
    public EmailData[] emailOptions;  // Assign EmailData assets in Inspector
    private EmailData currentEmail;
    private Renderer paperRenderer;
    private XRGrabInteractable grabInteractable;
    public float destroyDelay = 1.5f; // Time before disappearing after being placed

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        paperRenderer = GetComponent<Renderer>();

        if (paperRenderer == null)
        {
            Debug.LogError("No Renderer found on the Paper object!");
            return;
        }

        // Ensure Renderer is enabled
        paperRenderer.enabled = true;

        if (emailOptions == null || emailOptions.Length == 0)
        {
            Debug.LogError("emailOptions array is empty! Assign EmailData assets in the Inspector.");
            return;
        }

        AssignRandomEmail();
    }

    private void AssignRandomEmail()
    {
        if (emailOptions.Length > 0)
        {
            int randomIndex = Random.Range(0, emailOptions.Length);
            currentEmail = emailOptions[randomIndex];

            if (currentEmail != null && currentEmail.emailTexture != null)
            {
                // Ensure the paper has a material
                if (paperRenderer.material != null)
                {
                    // Assign the texture to the material’s main texture slot
                    paperRenderer.material.mainTexture = currentEmail.emailTexture;
                }
                else
                {
                    Debug.LogError("Paper material is missing! Assign a default material to the paper.");
                }
            }
            else
            {
                Debug.LogError("EmailData is missing or does not have a texture.");
            }
        }
        else
        {
            Debug.LogError("emailOptions is empty! Make sure to assign EmailData objects.");
        }
    }


    public string GetEmailType()
    {
        return currentEmail != null ? currentEmail.emailType : "Unknown";
    }

    [System.Obsolete]
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Paper collided with: " + other.gameObject.name);

        if (other.CompareTag("Bin") || other.CompareTag("Box"))
        {
            Debug.Log("Paper placed in: " + other.gameObject.name);

            // Check if it's being held and release it
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            if (grabInteractable && grabInteractable.isSelected)
            {
                grabInteractable.interactionManager.SelectExit(grabInteractable.selectingInteractor, grabInteractable);
                Debug.Log("Paper released from player's hand.");
            }

            // Check if the sorting is correct
            int points = 0;

            if (other.CompareTag("Bin") && currentEmail.emailType == "Phishing")
            {
                points = 1; // Correctly discarded phishing email
            }
            else if (other.CompareTag("Box") && currentEmail.emailType == "Safe")
            {
                points = 1; // Correctly stored a safe email
            }
            else
            {
                points = -1; // Wrong sorting
            }

            Debug.Log("Points Awarded: " + points);
            GameManager.Instance.AddScore(points);

            // Destroy the paper after a short delay
            Invoke("DestroyPaper", 0.5f);
        }
    }




    private void DestroyPaper()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.SpawnNewPaper(); // Tell GameManager to spawn a new paper
        }
        else
        {
            Debug.LogError("GameManager Instance is null! Make sure GameManager is in the scene.");
        }

        Destroy(gameObject); // Remove this paper from the scene
    }
}
