using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketPasswordCollector : MonoBehaviour
{
    [SerializeField] private PasswordManager passwordManager;
    // Reference to the socket interactor
    [SerializeField] private XRSocketInteractor socket;

    private void OnEnable()
    {
        // Subscribe to the socket’s selectEntered event
        socket.selectEntered.AddListener(OnBlockPlaced);
    }

    private void OnDisable()
    {
        // Unsubscribe when script/obj is disabled
        socket.selectEntered.RemoveListener(OnBlockPlaced);
    }

    private void OnBlockPlaced(SelectEnterEventArgs args)
    {
        // The interactable is the object that was placed in the socket
        IXRSelectInteractable interactable = args.interactableObject;

        // Get the CharBlock component from the object’s transform
        CharBlock block = interactable.transform.GetComponent<CharBlock>();
        if (block != null)
        {
            Debug.Log($"[SocketPasswordCollector] Block placed: {block.Character}");
            // 1) Add the character to the password
            passwordManager.AddCharacter(block.Character);

            // 2) Destroy the block to remove it from the socket
            Destroy(interactable.transform.gameObject);

            // At this point, the socket is free again to accept new blocks
        }
        else
        {
            Debug.Log("[SocketPasswordCollector] Placed object does NOT have a CharBlock component.");
        }
    }
}
