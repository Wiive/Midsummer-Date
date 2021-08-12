using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;
    public static OptionsManager Instnace { get { return instance; } }

    [System.Serializable]
    public class SaveData
    {
        public float soundVolume = 50f;
        public SpeedType textSpeed = SpeedType.Quick;
        public DayInfo dayInfo;
        public int loveMeter;
        //private Backlog backlog;
    }

    [Serializable]
    public class DayInfo
    {
        public int day = 1;
        public int time = 0;

        public DayInfo(int currentDay, int currentTime)
        {
            day = currentDay;
            time = currentTime;
        }
    }

    public SaveData myData;
    private string saveFilePath;

    public CanvasGroup panelCanvas;
    public TextMeshProUGUI volumeValue;
    public Slider volume;
    public ToggleGroup toggleValue;

    [Serializable]
    public enum SpeedType { Immersive, Vanilla, Quick, Sonic };
    private SpeedType currentType;
    private float immersive = 0.05f;
    private float vanilla = 0.025f;
    private float quick = 0.005f;
    private float sonic = 0f;

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

        saveFilePath = Application.persistentDataPath + "/savefile.json";

        myData = new SaveData();
        myData.dayInfo = new DayInfo(1,0);

        LoadFromData();

        Setup();
    }

    private void Start()
    {       
        panelCanvas.alpha = 0;
        panelCanvas.blocksRaycasts = false;
    }


    public void SaveToData() //I should seperate options saves and game progression saved UwU, but I do want options menu to have backlog, binded to the progression!
    {
        myData = new SaveData();
        myData.soundVolume = volume.value;
        myData.textSpeed = currentType;

        myData.loveMeter = DialogueManager.Instance.GetLoveValue();
        myData.dayInfo = new DayInfo(DayManager.Instance.CurrentDay, (int)DayManager.Instance.currentTime);
        

        string json = JsonUtility.ToJson(myData);

        File.WriteAllText(saveFilePath, json);
    }

    public void LoadFromData()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData myData = JsonUtility.FromJson<SaveData>(json);

            this.myData = myData;
        }
    }

    public void BackToMainMenu() //Might remove becuse we dont need that button?
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();
        levelManager.FadeToLevel(0);
        panelCanvas.alpha = 0;
        panelCanvas.blocksRaycasts = false;
    }

    //Update the current state on toggle change in options panel. Maybe bad that they have the same name? v
    public void UpdateTypeSpeed(int speedType)
    {
        switch((SpeedType)speedType)
        {
            case SpeedType.Immersive:
                currentType = SpeedType.Immersive;
                break;

            case SpeedType.Vanilla:
                currentType = SpeedType.Vanilla;
                break;

            case SpeedType.Quick:
                currentType = SpeedType.Quick;
                break;

            case SpeedType.Sonic:
                currentType = SpeedType.Sonic;
                break;
        }
    }

    //Returns the current speed for the right state. Maybe bad that they have the same name? ^
    public float UpdateTypeSpeed(Enum speedType)
    {
        switch (speedType)
        {
            case SpeedType.Immersive:
                return immersive;

            case SpeedType.Vanilla:
                return vanilla;

            case SpeedType.Quick:
                return quick;

            case SpeedType.Sonic:
                return sonic;
            default:
                return quick;
        }
    }

    public void Setup()
    {
        volume.value = myData.soundVolume;
        UpdateVolumeText();
        currentType = myData.textSpeed;
        UpdateToggle(currentType);
    }

    public void UpdateVolumeText()
    {
        volumeValue.text = volume.value.ToString();
    }

    public void UpdateToggle(SpeedType currentType)
    {
        var toggles = toggleValue.GetComponentsInChildren<Toggle>();
        Toggle activated = toggles[(int)currentType];
        activated.isOn = true;
        foreach (Toggle toggle in toggles)
        {
            if (toggle != activated)
            {
                toggle.isOn = false;
            }
        }
    }

    public void ResetOptions()
    {
        myData = new SaveData();
        myData.textSpeed = SpeedType.Quick;
        myData.loveMeter = DialogueManager.Instance.GetLoveValue();
        myData.dayInfo = new DayInfo(DayManager.Instance.CurrentDay, (int)DayManager.Instance.currentTime);
        
        string json = JsonUtility.ToJson(myData);

        File.WriteAllText(saveFilePath, json);

        Setup();
    }

    public void ResetGame()
    {
        myData = new SaveData();
        myData.dayInfo = new DayInfo(1,0);
        DialogueManager.Instance.ResetLoveMeter();
        DayManager.Instance.UpdateTime();

        string json = JsonUtility.ToJson(myData);

        File.WriteAllText(saveFilePath, json);

        Setup();
    }
}
