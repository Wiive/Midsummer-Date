using UnityEngine;
using UnityEngine.UI;

public class UpdateHUDOnClick : MonoBehaviour
{
    private Button button;
    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(UpdateHUD);
    }

    public void UpdateHUD()
    {
        HUDManager hudManager = FindObjectOfType<HUDManager>();
        Canvas canvas = FindObjectOfType<ActivitiesManager>().GetComponentInChildren<Canvas>();
        hudManager.UpdateCanvas(canvas);
    }
}
