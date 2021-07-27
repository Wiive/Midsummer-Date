using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    private static OptionsManager instance;
    public static OptionsManager Instnace { get { return instance; } }

    private class SaveData
    { 

    }
    SaveData saveData;

    public CanvasGroup panelCanvas;


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

    public void ResetOptions()
    {

    }
}
