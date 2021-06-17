using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitiesManager : MonoBehaviour
{
    public List<GameObject> day1ActivitiesButtons;
    public List<GameObject> day2ActivitiesButtons;
    public List<GameObject> day3ActivitiesButtons;

    [SerializeField] List<GameObject> allActivitesButtons;
    private void Start()
    {
        if (day1ActivitiesButtons.Count == 0)
        {
            Debug.LogError("No Day 1 Activites are imported to " + gameObject.name);
        }
        if (day2ActivitiesButtons.Count == 0)
        {
            Debug.LogError("No Day 2 Activites are imported to " + gameObject.name);
        }
        if (day3ActivitiesButtons.Count == 0)
        {
            Debug.LogError("No Day 3 Activites are imported to " + gameObject.name);
        }

        allActivitesButtons.AddRange(day1ActivitiesButtons);
        allActivitesButtons.AddRange(day2ActivitiesButtons);
        allActivitesButtons.AddRange(day3ActivitiesButtons);

        foreach (GameObject gameObject in allActivitesButtons)
        {           
            Button button = gameObject.GetComponent<Button>();
            button.onClick.AddListener(HideAllActivites);
        }

        HideAllActivites();

        UpdateActivities();
    }

    public void HideAllActivites()
    {
        foreach (GameObject gameObject in allActivitesButtons)
        {
            gameObject.SetActive(false);
        }
    }

    //Maybe implement a way to see wath time of the day it is here to
    //And make UpdateActivites to a switch, much better to scale and probebly better performance?

    public void UpdateActivities()
    {
        if (DayManager.Instance.CurrentDay == 1)
        {
            foreach (GameObject gameObject in day1ActivitiesButtons)
            {
                gameObject.SetActive(true);
            }
        }

        else if (DayManager.Instance.CurrentDay == 2)
        {
            foreach (GameObject gameObject in day2ActivitiesButtons)
            {
                gameObject.SetActive(true);
            }
        }

        else if (DayManager.Instance.CurrentDay == 3)
        {
            foreach (GameObject gameObject in day3ActivitiesButtons)
            {
                gameObject.SetActive(true);
            }
        }
    }
}
