using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggleAnimator : MonoBehaviour
{
    public List<GameObject> animationObjects;

    public void ToggleAnimator(bool boolean)
    {
        foreach(GameObject animationObject in animationObjects)
        {
            animationObject.SetActive(boolean);
            Animator animator = animationObject.GetComponent<Animator>();

            if (animator != null)
            {
                animator.enabled = boolean;
            }
        }
    }
}
