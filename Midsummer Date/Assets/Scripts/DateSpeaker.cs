using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DateSpeaker : Speaker
{
    public float loveValue;
    public Slider loveMeter;

    public void StartLoveDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
        loveValue += 1 * 0.1f;
        loveMeter.value = loveValue;

        if (gameObject.GetComponent<Button>() == true)
        {
            Destroy(gameObject);
        }
    }

}
