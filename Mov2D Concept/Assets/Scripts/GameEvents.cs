using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    public void Awake()
    {
        current = this;
    }

    public event Action onMapInstance;
    public void MapInstanceEvent()
    {
        if (onMapInstance != null)
            onMapInstance();
    }
}
