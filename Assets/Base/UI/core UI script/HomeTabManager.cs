using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomeTabManager : Singleton<HomeTabManager>
{
    [SerializeField] private UIScreen[] tabScreens;

    [SerializeField] private IntGameEvent tabChangeEvent;

    public bool isHome;

    private void Start()
    {
        tabChangeEvent.AddListener(ChangeTabByIndex);

        UIScreen screen;
        int activeIndex = -1;
        for (int i = 0; i < tabScreens.Length; i++)
        {
            screen = tabScreens[i];
            if (screen != null && screen.isActive)
            {
                if (activeIndex < 0)
                {
                    activeIndex = i;
                }
                else
                {
                    Debug.LogError("There are more than 1 UI screen init at the start");
                    return;
                }
            }
        }
        if (activeIndex >= 0)
        {
            tabChangeEvent.Raise(activeIndex);
        }
    }

    [ReadOnly] [SerializeField] public UIScreen _current;
    public void ChangeTabByIndex(int index)
    {
        if (tabScreens[index] != _current)
        {
            _current?.ToggleOff();
            _current = tabScreens[index];
            _current?.ToggleOn();
        }
        if(index == 6)
        {
            isHome = true;
        }
        else isHome = false;
    }
}
