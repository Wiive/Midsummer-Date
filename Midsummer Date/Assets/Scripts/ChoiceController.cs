using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;

[System.Serializable]
public class ConversationChangeEvent : UnityEvent<Conversation> {}

public class ChoiceController : MonoBehaviour
{
    public Choice choice;
    public ConversationChangeEvent conversationChangeEvent;

    public static ChoiceController AddChoiceButton(Button choiceButton, Choice choice, int index)
    {
        int buttonSpacing = -30;
        Button button = Instantiate(choiceButton);

        button.transform.SetParent(choiceButton.transform.parent);
        button.transform.localScale = Vector3.one;
        button.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        button.name = "Choice " + (index + 1);
        button.gameObject.SetActive(true);

        ChoiceController choiceController = button.GetComponent<ChoiceController>();
        choiceController.choice = choice;
        return choiceController;
    }

    private void Start()
    {
        if (conversationChangeEvent == null)
        {
            conversationChangeEvent = new ConversationChangeEvent();
        }

        GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
    }

    public void MakeChoice()
    {
        DialogueManager.Instance.StartConversation(choice.conversation);
    }
}
