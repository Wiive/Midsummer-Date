using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;
    public static OptionsManager Instnace { get { return instance; } }

    private class SaveData
    {
        private float soundVolume;
        private float textSpeed;
        private int loveMeter;
        //private Backlog backlog;
    }
    SaveData saveData;

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
    }

    private void Start()
    {       
        panelCanvas.alpha = 0;
        panelCanvas.blocksRaycasts = false;

        if (saveData != null)
        {
            LoadFromData();
        }
        UpdateVolumeText();
    }

    public void SaveToData()
    {
        //Make saveData into a json file.
    }

    public void LoadFromData()
    {
        //Load saveData from json file.
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

    public void UpdateVolumeText()
    {
        volumeValue.text = volume.value.ToString();
    }

    public void ResetOptions()
    {

    }
}
