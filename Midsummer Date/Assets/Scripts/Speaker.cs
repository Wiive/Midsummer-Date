using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Speaker : MonoBehaviour
{
    public Dialogue dialogue;
    public Conversation conversation;

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    public void StartConversation()
    {
        DialogueManager.Instance.StartConversation(conversation);
    }
}
