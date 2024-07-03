using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface ITooltip
{
    void Show();
}

[RequireComponent(typeof(UI_ItemBase))]
public class ShowTooltip : MonoBehaviour, ITooltip
{
    IDataHolder<GameItem> dataHolder;
    RectTransform rect;
    private void Awake()
    {
        dataHolder = GetComponent<IDataHolder<GameItem>>();
        rect = transform as RectTransform;
    }

    public void Show()
    {
        if (dataHolder != null)
        {
            Debug.Log("Show tooltip of " + dataHolder.info.Name);
        }
    }
}


