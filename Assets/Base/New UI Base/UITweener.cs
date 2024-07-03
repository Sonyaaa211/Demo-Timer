using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UITweener : MonoBehaviour, IVisualize
{
    public abstract void VisualOff(Action callback = null);

    public abstract void VisualOn(Action callback = null);
}
