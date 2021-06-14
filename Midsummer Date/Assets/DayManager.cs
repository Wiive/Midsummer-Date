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
    TimeOfDay currentTime;

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
        dateString = date.ToString() + dateString;
        dateText.text = dateString;

        lightSettings = lightSettings.GetComponent<Light>();
        lightsTransfom = lightSettings.GetComponent<Transform>();
        UpdateTime();
    }

    public void UpdateTime()
    {
        //Get values from jsonfile when we have a saving funktions

    }
}
