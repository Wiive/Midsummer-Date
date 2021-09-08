using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    private Animator animator;
    private string currentState;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation(string newState)
    {
        if (currentState == newState)
        {
            return;
        }
        currentState = newState;
        animator.SetTrigger(newState);
    }
}
