using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityExtensions.Tween;

public class UINavigator : UI_EventHolder<UITabItem>, IDriven
{
    [Range(0, 0.05f)][SerializeField] private float offset;

    [SerializeField] private IntGameEvent tabChangeEvent;

    Vector2 anchorMin = new Vector2(1, 0);
    Vector2 anchorMax = new Vector2(1, 1);
    [BoxGroup("Tween")][SerializeField] protected TweenPlayer TP;
    private bool _isShowAllButton = false;
    public void OnDriven()
    {
        //items = GetComponentsInChildren<UITabItem>();
        
        //int n = items.Length;
        //float delta = 1f / n;

        //for (int i = 0; i < n; i++)
        //{
        //    anchorMin.x = delta * i + (i != 0 ? offset: 0);
        //    items[i].rect.anchorMin = anchorMin;

        //    anchorMax.x = delta * (i + 1) - ((i != n - 1) ? offset : 0);
        //    items[i].rect.anchorMax = anchorMax;
        //}
    }

    UITabItem _current;
    public void ShowAllButton()
    {
        _isShowAllButton = !_isShowAllButton;
        if (_isShowAllButton)
        {
            TP.ForcePlayRuntime();
        }
        else
        {
            TP.ForcePlayBackRuntime();
            _current = null;
        }
    }
    public void ToggleOn()
    {
        GetComponent<UIScreen>().ToggleOn();
    }
    protected override void ClickItem(UITabItem item)
    {
        if (item == _current) { ShowAllButton(); return; }
        else
        {
            _current?.OnUnpicked();
            _current = item;
            _current.OnPicked();
            tabChangeEvent.Raise(_current.tabIndex);
            ShowAllButton();
        }
    }
}
