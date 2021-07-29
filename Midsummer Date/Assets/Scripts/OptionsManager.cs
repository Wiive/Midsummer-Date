using System.Collections.Generic;
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
        public float textSpeed = 0.005f;
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


    public void SaveToData()
    {
        myData = new SaveData();
        myData.soundVolume = volume.value;
        myData.textSpeed = 0.005f; //Take from speeds in the toggle menu

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

    private void UpdateTypeSpeed()
    {
        //For types of speed:
        // Imersive
        // Vanilla
        // Quick
        // Sonic
    }

    public void Setup()
    {
        volume.value = myData.soundVolume;
        UpdateVolumeText();
        //maybe set dialougemanagers typespeed, if i want it to be "public"
    }

    public void UpdateVolumeText()
    {
        volumeValue.text = volume.value.ToString();
    }

    public void ResetOptions()
    {
        myData = new SaveData();
        myData.loveMeter = DialogueManager.Instance.GetLoveValue();
        myData.dayInfo = new DayInfo(DayManager.Instance.CurrentDay, (int)DayManager.Instance.currentTime);

        string json = JsonUtility.ToJson(myData);

        File.WriteAllText(saveFilePath, json);

        Setup();
    }

    public void ResetGame()
    {
        myData = new SaveData();
        
        string json = JsonUtility.ToJson(myData);

        File.WriteAllText(saveFilePath, json);
    }
}
