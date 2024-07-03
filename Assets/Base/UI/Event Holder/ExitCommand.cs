using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitCommand : MonoBehaviour
{

    public void OnExit()
    {
        var visual = GetComponentInParent<IVisualize>();
        if (visual != null)
        {
            visual.VisualOff();
        }
        else
        {
            var toggle = GetComponentInParent<IToggleable>();
            if (toggle != null)
            {
                toggle.ToggleOff();
            }
        }
    }
}
