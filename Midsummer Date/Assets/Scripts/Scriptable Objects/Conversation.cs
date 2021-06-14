using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Dialogue speaker1;
    public Dialogue speaker2;

    public Question question;
    public Conversation nextConversation;

    //Make it able to stop with a Chocie / Question
}
