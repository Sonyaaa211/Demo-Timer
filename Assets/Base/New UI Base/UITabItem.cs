using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class UITabItem : UI_Item, IDriven
{
    public int tabIndex => transform.GetSiblingIndex();

    IDriven _parentDriver;
    public void OnDriven()
    {
        _parentDriver = transform.parent?.GetComponent<IDriven>();
        
        if (_parentDriver != null)
        {
            _parentDriver.OnDriven();
        }
    }

    public override void OnPicked()
    {
        
    }

    public override void OnUnpicked()
    {
        //GetComponentInParent<UIScreen>().VisualOff();
    }
}
