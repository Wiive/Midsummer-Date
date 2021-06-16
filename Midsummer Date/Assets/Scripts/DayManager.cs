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
        currentTime = TimeOfDay.Midday;
        timeText.text = currentTime.ToString();

        lightSettings = lightSettings.GetComponent<Light>();
        lightsTransfom = lightSettings.GetComponent<Transform>();

        UpdateTime();
    }

    public void UpdateTime()
    {
        //Get values from jsonfile when we have a saving funktions
    }

    public void ChangeTime(int timeIndex)
    {
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

    public void NextDay()
    {
        date++;
        dateText.text = date.ToString() + dateString;
    }
}
