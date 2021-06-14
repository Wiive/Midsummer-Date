using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionController : MonoBehaviour
{
    public Question question;
    public TextMeshProUGUI questionText;
    public Button choiceButton;

    private List<ChoiceController> choiceControllers = new List<ChoiceController>();

    public void Change(Question question)
    {
        RemoveChoices();
        this.question = question;
        gameObject.SetActive(true);
        SetUpChoices();
    }

    public void HideQuestions()
    {
        RemoveChoices();
        gameObject.SetActive(false);
    }

    public void RemoveChoices()
    {
        foreach(ChoiceController controller in choiceControllers)
        {
            Destroy(controller.gameObject);
        }

        choiceControllers.Clear();
    }

    private void SetUpChoices()
    {
        questionText.text = question.text;

        for (int i = 0; i < question.choices.Length; i++)
        {
            ChoiceController controller = ChoiceController.AddChoiceButton(choiceButton, question.choices[i], i);
            choiceControllers.Add(controller);
        }

        //Hide Choice Button Templet
        choiceButton.gameObject.SetActive(false);
    }
}
