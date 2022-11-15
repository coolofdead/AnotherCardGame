using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstractManager<T> : MonoBehaviour where T : Component
{
    public static T Instance;

    protected virtual void Awake()
    {
        Instance = this as T;
    }
}
