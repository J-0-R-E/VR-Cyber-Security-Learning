using UnityEngine;
using TMPro;  // If you're using TextMeshPro for text display

public class PasswordManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI passwordDisplay;

    // We'll build the password string from player-collected characters.
    private string currentPassword = "";

    // Requirements flags
    private bool hasUpperCase;
    private bool hasLowerCase;
    private bool hasDigit;
    private bool hasSpecial;

    private void Start()
    {
        Debug.Log("[PasswordManager] Start: Initializing PasswordManager.");
        UpdatePasswordDisplay();
    }

    // Method to add a character to the password
    public void AddCharacter(char c)
    {
        Debug.Log($"[PasswordManager] AddCharacter called with '{c}'");
        currentPassword += c;
        CheckCharacterType(c);

        Debug.Log($"[PasswordManager] Current password after adding '{c}': {currentPassword}");
        UpdatePasswordDisplay();
    }

    // Check if a certain character meets one of the criteria
    private void CheckCharacterType(char c)
    {
        if (char.IsUpper(c))
        {
            hasUpperCase = true;
            Debug.Log($"[PasswordManager] Found uppercase: '{c}'");
        }
        if (char.IsLower(c))
        {
            hasLowerCase = true;
            Debug.Log($"[PasswordManager] Found lowercase: '{c}'");
        }
        if (char.IsDigit(c))
        {
            hasDigit = true;
            Debug.Log($"[PasswordManager] Found digit: '{c}'");
        }
        // For demonstration, let's consider any non-alphanumeric character as special
        if (!char.IsLetterOrDigit(c))
        {
            hasSpecial = true;
            Debug.Log($"[PasswordManager] Found special character: '{c}'");
        }
    }

    // Clear the password if the player chooses to reset
    public void ClearPassword()
    {
        Debug.Log("[PasswordManager] ClearPassword called. Resetting all character flags.");
        currentPassword = "";
        hasUpperCase = false;
        hasLowerCase = false;
        hasDigit = false;
        hasSpecial = false;
        UpdatePasswordDisplay();
    }

    // Display the current password on a UI element
    private void UpdatePasswordDisplay()
    {
        Debug.Log($"[PasswordManager] UpdatePasswordDisplay: currentPassword = '{currentPassword}'");
        if (passwordDisplay != null)
        {
            passwordDisplay.text = currentPassword;
        }
        else
        {
            Debug.LogWarning("[PasswordManager] passwordDisplay is not assigned. Cannot show password on UI.");
        }
    }

    // Check if the password meets the requirements
    public bool IsPasswordStrong()
    {
        bool meetsLengthRequirement = currentPassword.Length >= 8;
        bool isStrong = meetsLengthRequirement && hasUpperCase && hasLowerCase && hasDigit && hasSpecial;

        Debug.Log($"[PasswordManager] IsPasswordStrong called. " +
                  $"Length >= 8? {meetsLengthRequirement}, " +
                  $"Upper? {hasUpperCase}, Lower? {hasLowerCase}, " +
                  $"Digit? {hasDigit}, Special? {hasSpecial}. " +
                  $"Result: {isStrong}");

        return isStrong;
    }
}
