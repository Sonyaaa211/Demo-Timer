using Hung.Pooling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type_HealthBar : ICommonPoolable
{
    public Transform Storage { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public bool enabled { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    [field:SerializeField] public GameObject gameObject { get; private set; }

    public Transform transform => throw new System.NotImplementedException();

    public void BackToPool()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleOff()
    {
        throw new System.NotImplementedException();
    }

    public void ToggleOn()
    {
        throw new System.NotImplementedException();
    }
}

[RequireComponent(typeof(UIProcessBar))]
public class UIHealthBar : ObjectFollower<Unit>, IResizable, ICommonPoolable
{
    [SerializeField] protected UIProcessBar bar;
    [SerializeField] private float appearTime;
    private float _appearTime = 0;

    public float size
    {
        get
        {
            return Model.transform.localScale.magnitude;
        }
        set
        {
            Model.transform.localScale = Vector3.one * value;
        }
    }

    [field: SerializeField] public GameObject Model {get; private set;}

    private void Start()
    {
        VisibleCheck += TimeCheck;
    }

    private bool TimeCheck()
    {
        _appearTime -= Time.deltaTime;
        return _appearTime >= 0;
    }

    public float current
    {
        set
        {
            bar.InstantSet(value);
        }
    }
    
    internal override void SetFollow(Unit unit)
    {
        gameObject.SetActive(true);
        target = unit.FollowTarget;

        size = unit.visualSize;

        bar.InstantSet(unit.percentHP);
        //Debug.Log(unit.percentHP);
        unit.OnHealthChanged += OnHealthChanged;
    }

    internal override void Unfollow()
    {
        target = null;
        BackToPool();
    }

    internal void OnHealthChanged(float changedPercent)
    {
        //Debug.Log(changedPercent); 
        bar.VisualChange(changedPercent);
        _appearTime = appearTime;
    }    
}
