using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIScreenManager : Singleton<UIScreenManager>, IReset
{
    [SerializeField] private List<UIScreen> m_screens;
   
    public void OnReset()
    {
        m_screens = TypeFinder.FindMultiComponents<UIScreen>(isIncludeInactive: true);
    }

    public void Awake()
    {
        m_screens.ForEach(screen =>
        {
            screen.OnStart();           
        });
    }
}
