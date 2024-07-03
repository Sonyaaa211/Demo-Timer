using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IReset: IMono
{
    void OnReset();
}

public interface IMono
{
    bool enabled { get; set; }

    Type GetType();

    GameObject gameObject { get; }

    Transform transform { get; }
}