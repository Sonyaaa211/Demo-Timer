using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualEffect3D : DisableByTime, IResizable
{
    [field:SerializeField] public GameObject Model { get; private set; }
    public float size 
    { 
        get => Model.transform.localScale.x;

        set => Model.transform.localScale = Vector3.one * value;
    }

    public float duration => Lifetime;
}
