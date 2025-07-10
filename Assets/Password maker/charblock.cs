using UnityEngine;
using TMPro;

public class CharBlock : MonoBehaviour
{
    [SerializeField] private TextMeshPro text3D;
    [SerializeField] private char character = 'A'; // default value for Inspector

    public char Character => character;

    // Optionally, call this at Start to ensure it updates once when the game starts.
    private void Start()
    {
        UpdateCharacterText();
    }

    // Method to set the character at runtime
    public void SetCharacter(char newChar)
    {
        character = newChar;
        UpdateCharacterText();
    }

    // Update the displayed text to match `character`
    private void UpdateCharacterText()
    {
        if (text3D != null)
        {
            text3D.text = character.ToString();
        }
    }
}
