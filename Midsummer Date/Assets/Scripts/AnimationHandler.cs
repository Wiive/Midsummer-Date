using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private string currentState;
    private AnimationTrigger animationTrigger;

    public DateEmotions emotion;

    private void Start()
    {
        animator = GetComponent<Animator>();
        emotion = DateEmotions.Idle;
        animationTrigger = new AnimationTrigger(emotion);
    }

    public void TriggerAnimation(AnimationTrigger trigger)
    {
        string newState = trigger.emotion;
        if (currentState == newState)
        {
            return;
        }
        currentState = newState;
        animator.SetTrigger(newState);
    }
}
