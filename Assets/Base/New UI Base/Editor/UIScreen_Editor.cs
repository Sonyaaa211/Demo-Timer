using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(UIScreen))]
public class UIScreen_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ((UIScreen)target).OnDriven();   
    }
}

