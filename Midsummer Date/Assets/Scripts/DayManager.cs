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
        lightSettings = lightSettings.GetComponent<Light>();
        lightsTransfom = lightSettings.GetComponent<Transform>();

        UpdateTime();
    }

    public void UpdateTime()
    {
        currentDay = OptionsManager.Instnace.myData.dayInfo.day;
        currentTime = (TimeOfDay)OptionsManager.Instnace.myData.dayInfo.time;
        date = date + (currentDay - 1);
        dateText.text = date.ToString() + dateString;
        timeText.text = currentTime.ToString();
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
                OptionsManager.Instnace.myData.dayInfo.time = (int)currentTime;
                break;

            case TimeOfDay.Midday:
                timeText.text = currentTime.ToString();
                lightsTransfom.rotation = Quaternion.Euler(50, -30, 0);
                OptionsManager.Instnace.myData.dayInfo.time = (int)currentTime;
                break;

            case TimeOfDay.Evening:
                timeText.text = currentTime.ToString();
                lightsTransfom.rotation = Quaternion.Euler(140, -30, 0);
                OptionsManager.Instnace.myData.dayInfo.time = (int)currentTime;
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
        OptionsManager.Instnace.myData.dayInfo.day = currentDay;
        OptionsManager.Instnace.SaveToData();

        DayInformation newDay = ScriptableObject.CreateInstance<DayInformation>();
        InformationWindow.Instance.NewInformation(newDay);
    }

    public void ShowActivitesCanvas()
    {
        ActivitiesManager activitiesManager = FindObjectOfType<ActivitiesManager>();

        Canvas canvas = activitiesManager.GetComponentInChildren<Canvas>();
        canvas.enabled = true;

        activitiesManager.UpdateActivities();
    }  
}
