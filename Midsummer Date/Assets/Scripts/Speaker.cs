using UnityEngine;
using UnityEngine.UI;

public abstract class Speaker : MonoBehaviour
{
    public Dialogue dialogue;
    public Conversation conversation;

    public void StartDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);

        if (gameObject.GetComponent<Button>() == true)
        {
            gameObject.SetActive(false);
        }
    }

    public void StartConversation()
    {
        DialogueManager.Instance.StartConversation(conversation);

        if (gameObject.GetComponent<Button>() == true)
        {
            gameObject.SetActive(false);
        }
    }
}
