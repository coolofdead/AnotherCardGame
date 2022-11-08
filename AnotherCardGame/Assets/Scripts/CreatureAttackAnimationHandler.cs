using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using System;

public class CreatureAttackAnimationHandler : MonoBehaviour
{
    public Action onAttackAnimationFinished;

    public PlayableDirector attackAnimationTimeline;

    public void PlayAttackAnimation()
    {
        attackAnimationTimeline.Play();
    }

    public void AnimationFinished()
    {
        onAttackAnimationFinished?.Invoke();

        Destroy(gameObject);
    }
}
