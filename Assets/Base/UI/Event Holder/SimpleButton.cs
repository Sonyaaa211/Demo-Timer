using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasRenderer))]
public class SimpleButton : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IToggleable
{
    [SerializeField] private UnityEvent clickEvent;
    [SerializeField] private List<PointerDownEvent> pointerDownEvents;
    [SerializeField] internal RectTransform eventTarget;
    [SerializeField] private ClickSoundType soundType;
    [SerializeField] private bool hasInterAds;
    [SerializeField] private Image mask;

    private Action<int> action;

    public UnityEvent ClickEvent { get => clickEvent; set => clickEvent = value; }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (hasInterAds && IngameController.Instance.CheckInterAdsWithReturn()) return;
        //AudioController.Instance.PlaySound(UISound.ButtonClick);
        clickEvent.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (PointerDownEvent downEvent in pointerDownEvents)
        {
            downEvent.Cast(this, eventData);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (PointerDownEvent downEvent in pointerDownEvents)
        {
            downEvent.Recast(this, eventData);
        }
    }

    public void Blur(bool isCounterCast = false)
    {
        enabled = isCounterCast;
        mask.gameObject.SetActive(!isCounterCast);
        //Debug.Log(isCounterCast); 
    }

    internal void ChangeIcon(Sprite icon)
    {
        eventTarget.GetComponent<Image>().sprite = icon;
    }

    public void ToggleOn()
    {
        gameObject.SetActive(true);
    }

    public void ToggleOff()
    {
        gameObject.SetActive(false);
    }
}

public enum ClickSoundType
{
    Default,
    Back,
    Scroll,
    None
}

public enum InterAdsScreen
{
    None,
    WinGame,
    LoseGame
}