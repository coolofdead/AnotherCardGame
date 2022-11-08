using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OnEnablePlayTimeline : MonoBehaviour
{
    [SerializeField] private PlayableDirector timeline;

    void OnEnable()
    {
        timeline.Play();
    }
}
