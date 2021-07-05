using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetUpActivity : MonoBehaviour
{
    public Activity activity;

    public TextMeshProUGUI title;
    public Image icon;

    private DateSpeaker trigger;
    private Button button;

    private void Start()
    {
        title = title.GetComponent<TextMeshProUGUI>();
        title.text = activity.title;

        icon = icon.GetComponent<Image>();
        icon.sprite = activity.picture;

        trigger = GetComponent<DateSpeaker>();
        if (trigger == null)
        {
            Debug.LogError("Missing DateSpeaker, the trigger for the activity");
        }
        else
        {
            trigger.conversation = activity.conversation;
        }

        button = GetComponent<Button>();
        button.onClick.AddListener(trigger.StartConversation);
    }
}
