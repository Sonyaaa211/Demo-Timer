using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProcess: IToggleable
{
    float progress { get; set; }

    void VisualChange(float toValue, float duration, Action endCallback = null);
}