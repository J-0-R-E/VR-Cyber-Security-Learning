using UnityEngine;

[CreateAssetMenu(fileName = "NewEmail", menuName = "Email System/Email Data")]
public class EmailData : ScriptableObject
{
    public string emailType;  // "Phishing" or "Safe"
    public Texture emailTexture;
}
