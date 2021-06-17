using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivitiesManager : MonoBehaviour
{
    public List<GameObject> activitiesButtons;

    private void Start()
    {
        if (activitiesButtons.Count == 0)
        {
            Debug.LogError("No Activites are imported to " + gameObject.name);
        }
        
        else
        {
            foreach (GameObject gameObject in activitiesButtons)
            {
                Button button = gameObject.GetComponent<Button>();
                button.onClick.AddListener(HideAllActivites);
            }
        }          
    }

    public void HideAllActivites()
    {
        foreach (GameObject gameObject in activitiesButtons)
        {
            gameObject.SetActive(false);
        }
    }
}
