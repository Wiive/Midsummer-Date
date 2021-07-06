using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationWindow : MonoBehaviour
{
    private static InformationWindow instance;

    public Information information;

    public TextMeshProUGUI title;
    public TextMeshProUGUI informationText;

    public Image image1;
    public Image image2;

    public static InformationWindow Instance { get { return instance; } }

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
        image1 = image1.GetComponent<Image>();
        image2 = image2.GetComponent<Image>();
        UpdateInformation();
    }

    public void NewInformation(Information information)
    {
        this.information = information;
        UpdateInformation();
    }

    public void UpdateInformation()
    {
        title.text = information.title;
        informationText.text = information.information;
        image1.sprite = information.image;
        image2.sprite = information.image;
    }
}
