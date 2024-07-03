using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisualize
{
    void VisualOn(Action callback = null);

    void VisualOff(Action callback = null);
}
