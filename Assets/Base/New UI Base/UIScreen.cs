using ScriptableObjectArchitecture;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Device;
using UnityEngine.EventSystems;
using UnityExtensions.Tween;

public class UIScreen : UIBehaviour, IForceStart, IToggleable, IVisualize, IDriven, ITest, IReset
{
    [SerializeField] private TweenPlayer m_tweenPlayer;
    [SerializeField] private GameEvent openEventCommand;
    [ReadOnly] [SerializeField] private List<GameObject> m_forceStarts;
    

    [field: SerializeField] public bool isInit { get; private set; }
    [field: SerializeField] public bool playOnAwake {  get; private set; }
    public bool isActive { get; private set; }

    public void OnDriven()
    {
        //m_tweener = GetComponent<UITweener>();
        m_tweenPlayer = GetComponent<TweenPlayer>();
        
    }

    public void OnReset()
    {
        m_forceStarts = GetComponentsInChildren<IForceStart>(true).ToList().Where(forceStart => !forceStart.Equals(this)).Select(forceStart => forceStart.gameObject).ToList();
    }

    public void OnStart()
    {
        openEventCommand?.AddListener(() => VisualOn());

        if (!isInit) ToggleOff();
        else VisualOn();

    }

    protected override void Awake()
    {
        m_forceStarts.ForEach(starter => starter.GetComponent<IForceStart>().OnStart());
    }

    public void ToggleOn()
    {
        gameObject.SetActive(true);
        isActive = true;
    }

    public void ToggleOff()
    {
        HomeTabManager.Instance._current = null;
        gameObject.SetActive(false);
        isActive = false;
    }

    public void VisualOn(Action callback = null)
    {
        Debug.Log("Screen " + name + " is visual on"); 
        ToggleOn();
        if(playOnAwake)
        m_tweenPlayer?.ForcePlayRuntime();
    }

    public void VisualOff(Action callback = null)
    {
        callback += ToggleOff;
        callback?.Invoke();
        if(playOnAwake)
        m_tweenPlayer?.ForcePlayBackRuntime();
    }

    public void OnTest()
    {
        VisualOn();
    }
}
