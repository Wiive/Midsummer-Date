using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DayInformation : Information
{
    private void Awake()
    {
        UpdateDateText();
    }

    public void UpdateDateText()
    {
        //This is BAD, not scalable and need a lot of editing if something should change.
        switch(DayManager.Instance.CurrentDay)
        {
            case 1:
                title = "22 June";
                information = "It's a new day! \r\n Two days before Midsummer Eve";
                break;
            case 2:
                title = "23 June";
                information = "It's a new day! \r\n Tomorrow is Midsummer Eve";
                break;
            case 3:
                title = "24 June";
                information = "It's a new day! \r\n Today is Midsummer Eve";
                break;
        }
    }
}
