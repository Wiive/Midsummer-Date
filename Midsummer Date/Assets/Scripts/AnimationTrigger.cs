using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationTrigger
{
    public string emotion;
    public AnimationTrigger(DateEmotions newEmotion)
    {
        emotion = newEmotion.ToString();
        if (emotion == null)
        {
            emotion = "Idle";
        }
    }  
}
