using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventsBus
{
    public static UnityEvent LevelFailed = new UnityEvent();
    public static UnityEvent LevelPassedSuccesfully = new UnityEvent();
}
