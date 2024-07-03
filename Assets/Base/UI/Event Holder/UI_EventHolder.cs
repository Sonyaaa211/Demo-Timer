using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class UI_EventHolder<T>: UI_EventHolder where T:IPickable
{
    protected T[] items;

    protected override bool ClickAction(PointerEventData eventData, GameObject clicked)
    {
        ClickItem(clicked.GetItem<T>());
        return true;
    }

    protected abstract void ClickItem(T item);
}

public abstract class UI_EventHolder : UIBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerExitHandler
{
    protected const string TAG_FirtItem = "FirstItem";
    protected const string TAG_SecondItem = "SecondItem";
    protected const string TAG_ThirdItem = "ThirdItem";

    [SerializeField] private ClickType clickType;
    private float m_scale;

    protected float PopScale
    {
        get
        {
            if (m_scale == 0)
            {
                m_scale = clickType == ClickType.PopDown ? popDownScale : popUpScale;
            }
            return m_scale;
        }
    }
    protected const float popUpScale = 1.1f;

    protected const float popDownScale = 0.9f;

    protected GameObject holding;

    protected bool wasDragging;

    [SerializeField] private bool ignoreDragProctector;
    //[SerializeField] private ClickSoundType defaultClick; //, firstItemClick, secondItemClick, thirdItemClick;

    protected abstract bool ClickAction(PointerEventData eventData, GameObject clicked);
    protected virtual void Up(Transform holding)
    {
        if (clickType != ClickType.None)
        {
            holding.transform.localScale = Vector3.one;
        }      
    }
    protected virtual void Down(Transform holding)
    {
        if (clickType != ClickType.None)
        {
            holding.transform.localScale = Vector3.one * PopScale;
        }       
    }


    private Action callback;
    public void OnPointerClickCallback(PointerEventData eventData = null, Action callback = null)
    {
        this.callback = callback;
        if (eventData != null)
        {
            OnPointerClick(eventData);
        }
    }

    public void AddCallback(Action callback)
    {
        this.callback += callback;
    }

    ITooltip _tooltip;
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject clicked = eventData.pointerCurrentRaycast.gameObject;

        if (clicked.tag == "EventHolder")
        {
            if (holding != null)
            {
                Up(holding.transform);
                holding = null;
            }
        }
        else if (clicked != holding)
        {
            if (holding != null) Up(holding.transform);
            holding = clicked;
        }
        else
        {
            Up(holding.transform);
            holding = null;
            if (ignoreDragProctector || (pos - Camera.main.ScreenToWorldPoint(Input.mousePosition)).magnitude < 1.0f)
            {
                if (ClickAction(eventData, clicked))
                {
                    if (clicked.TryGetComponent(out _tooltip))
                    {
                        _tooltip.Show();
                    }
                }
                if (callback != null)
                {
                    //Debug.Log("Kekw"); 
                    callback();
                    callback = null;
                }
                //Debug.Log("Click: " + clicked.name);
            }
        }
    }
    private Vector3 pos;
    public void OnPointerDown(PointerEventData eventData)
    {
        pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        GameObject downed = eventData.pointerCurrentRaycast.gameObject;
        //Debug.Log("Down: " + downed.tag);
        if (downed.tag != "EventHolder")
        {
            holding = downed;
            Down(holding.transform);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (holding != null)
        {
            Up(holding.transform);
        }
        holding = null;
    }
}

public enum ClickType
{
    PopDown,
    PopUp,
    None,
}

public static class UIItemGetter
{
    public static T GetItem<T>(this GameObject obj) where T: IPickable
    {
        return obj.GetComponentInParent<T>();
    }
}
