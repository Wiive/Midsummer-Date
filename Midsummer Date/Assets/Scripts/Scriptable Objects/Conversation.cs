using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public int loveScore;
    
    public Dialogue speaker1;
    public Dialogue speaker2;

    public Question question;
    public Conversation nextConversation;
}
