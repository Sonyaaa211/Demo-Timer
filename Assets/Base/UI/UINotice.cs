using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UINotice : MonoBehaviour, IToggleable
{
    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }

    public void ToggleOn()
    {
        gameObject.SetActive(true);
        //Pop tween
    }
}
