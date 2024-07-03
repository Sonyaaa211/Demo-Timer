using Hung.StatSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OutsourceModifier<T, U> : OutsourceModifier where T : StatChangerSource<U> where U: Stat
{
    
}


public abstract class OutsourceModifier : ScriptableObject
{
    [field:SerializeField] internal float defaultDuration {  get; private set; }

    internal abstract void OnModified(Amount currentAmount, IUnitStat unit);

    internal abstract void OnExpired(Amount currentAmount, IUnitStat unit);
}

