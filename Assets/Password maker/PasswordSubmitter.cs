using UnityEngine;
using UnityEngine.Events; // If you want to use UnityEvent for success/fail events

public class PasswordSubmitter : MonoBehaviour
{
    [SerializeField] private PasswordManager passwordManager;
    [SerializeField] private UnityEvent onPasswordStrong;
    [SerializeField] private UnityEvent onPasswordWeak;

    // Example method to call when the user activates this button/lever
    public void SubmitPassword()
    {
        bool isStrong = passwordManager.IsPasswordStrong();
        if (isStrong)
        {
            // Possibly celebrate with lights, SFX, etc.
            onPasswordStrong?.Invoke();
        }
        else
        {
            // Provide feedback about missing requirements
            onPasswordWeak?.Invoke();
        }
    }
}
