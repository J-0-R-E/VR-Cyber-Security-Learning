using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NotebookTeleport : MonoBehaviour
{
    public Transform teleportDestination; // First teleport location
    private XRGrabInteractable grabInteractable;
    private GameObject xrRig;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isAtTeleportDestination = false; // Tracks teleport state

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(TeleportPlayer);

        // Find XR Rig 
        xrRig = GameObject.Find("XR Rig");

        if (xrRig == null)
        {
            Debug.LogError("XR Origin not found! Make sure your XR Rig is named 'XR Origin' in the Hierarchy.");
        }
        else
        {
            // Store the original position when the game starts
            originalPosition = xrRig.transform.position;
            originalRotation = xrRig.transform.rotation;
        }
    }

    private void TeleportPlayer(SelectEnterEventArgs args)
    {
        if (xrRig == null)
        {
            Debug.LogError("XR Origin is missing!");
            return;
        }

        if (isAtTeleportDestination)
        {
            // Teleport back to the original position
            Debug.Log("Teleporting back to original position...");
            xrRig.transform.position = originalPosition;
            xrRig.transform.rotation = originalRotation;
        }
        else
        {
            // Teleport to the new location
            if (teleportDestination != null)
            {
                Debug.Log("Teleporting to new location...");
                xrRig.transform.position = teleportDestination.position;
                xrRig.transform.rotation = teleportDestination.rotation;
            }
            else
            {
                Debug.LogError("Teleport destination is not assigned!");
            }
        }

        // Toggle teleport state
        isAtTeleportDestination = !isAtTeleportDestination;
    }

    private void OnDestroy()
    {
        grabInteractable.selectEntered.RemoveListener(TeleportPlayer);
    }
}
