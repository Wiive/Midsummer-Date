using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DayManager : MonoBehaviour
{
    private static DayManager instance;

    public TextMeshProUGUI dateText;
    public TextMeshProUGUI timeText;
    public Light lightSettings;
    private Transform lightsTransfom;

    private int date = 22;
    private string dateString = " June ";

    private int currentDay = 1;
    public int CurrentDay { get { return currentDay; } }

    public enum TimeOfDay { Morning, Midday, Evening};
    public TimeOfDay currentTime;

    public static DayManager Instance { get { return instance; } }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        dateText.text = date.ToString() + dateString;
        timeText.text = currentTime.ToString();

        lightSettings = lightSettings.GetComponent<Light>();
        lightsTransfom = lightSettings.GetComponent<Transform>();

        //UpdateTime();
    }

    public void UpdateTime()
    {
        //Get values from jsonfile when we have a saving funktions
    }

    public void ChangeTime(int timeIndex)
    {
        if (timeIndex > 2)
        {
            timeIndex = 0;
        }
        currentTime = (TimeOfDay)timeIndex;
        switch (currentTime)
        {
            case TimeOfDay.Morning:
                NextDay();
                timeText.text = currentTime.ToString();
                lightsTransfom.rotation = Quaternion.Euler(-33, -30, 0);
                break;

            case TimeOfDay.Midday:
                timeText.text = currentTime.ToString();
                lightsTransfom.rotation = Quaternion.Euler(50, -30, 0);
                break;

            case TimeOfDay.Evening:
                timeText.text = currentTime.ToString();
                lightsTransfom.rotation = Quaternion.Euler(140, -30, 0);
                break;
        }
    }

    IEnumerator FadeLightning(int lightAngleValue, int newLightAngle)
    {
        if (lightAngleValue < newLightAngle)
        {
            lightAngleValue++;
        }
        else if (lightAngleValue > newLightAngle)
        {
            lightAngleValue--;
        }
        yield return new WaitForSeconds(0.01f);
    }

    private void NextDay()
    {
        currentDay++;
        date++;
        dateText.text = date.ToString() + dateString;

        DayInformation newDay = ScriptableObject.CreateInstance<DayInformation>();
        InformationWindow.Instance.NewInformation(newDay);


        //ShowActivitesCanvas();
    }

    public void ShowActivitesCanvas()
    {
        ActivitiesManager activitiesManager = FindObjectOfType<ActivitiesManager>();

        Canvas canvas = activitiesManager.GetComponentInChildren<Canvas>();
        canvas.enabled = true;

        activitiesManager.UpdateActivities();
    }  
}
