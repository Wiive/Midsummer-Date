using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class Speaker : MonoBehaviour
{
    public Dialogue dialogue;

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
