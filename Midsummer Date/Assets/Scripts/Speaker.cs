using UnityEngine;
using UnityEngine.UI;

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
        if (FindLoveMeter() != null)
        {
            UpdateLoveMeter();
        }
    }

    public Slider FindLoveMeter()
    {
        GameObject loveMeter = GameObject.FindGameObjectWithTag("LoveMeter");
        Slider loveMeterSlider = loveMeter.GetComponent<Slider>();
        return loveMeterSlider;
    }

    public void UpdateLoveMeter()
    {
        float loveValue = conversation.loveScore;
        GameObject loveMeter = GameObject.FindGameObjectWithTag("LoveMeter");
        Slider loveMeterSlider = loveMeter.GetComponent<Slider>();
        loveMeterSlider.value += loveValue;
    }
}
