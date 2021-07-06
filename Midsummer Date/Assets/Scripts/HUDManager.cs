using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public Canvas dialogueHUD;
    public Canvas dayManagerHUD;
    public Canvas levelManagerHUD;
    public Canvas activitiesManagerHUD;

    private List<Canvas> HUDList = new List<Canvas>();

    private void Start()
    {
        StartCoroutine(LoadInHUDs());
    }

    IEnumerator LoadInHUDs()
    {
        yield return new WaitForSeconds(0.5f);

        dayManagerHUD = FindObjectOfType<DayManager>().GetComponentInChildren<Canvas>();
        dialogueHUD = FindObjectOfType<DialogueManager>(true).GetComponentInChildren<Canvas>();
        levelManagerHUD = FindObjectOfType<LevelManager>().GetComponentInChildren<Canvas>();
        activitiesManagerHUD = FindObjectOfType<ActivitiesManager>().GetComponentInChildren<Canvas>();

        HUDList.Add(dayManagerHUD);
        HUDList.Add(dialogueHUD);
        HUDList.Add(levelManagerHUD);
        HUDList.Add(activitiesManagerHUD);

        yield return null;
    }

    public void UpdateCanvas(Canvas HUD)
    {
        foreach(Canvas canvas in HUDList)
        { 
            if (canvas == HUD)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }
    }

    public void UpdateCanvas()
    {
        levelManagerHUD.enabled = true;
        dayManagerHUD.enabled = true;
    }

    public void DialogueCanvas()
    {
        foreach (Canvas canvas in HUDList)
        {
            if (canvas == dialogueHUD)
            {
                canvas.enabled = true;
            }
            else
            {
                canvas.enabled = false;
            }
        }
    }
}
