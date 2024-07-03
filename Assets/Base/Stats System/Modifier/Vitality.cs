using Hung.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hung/Source Modifier/Outsource/Healing/Vitality")]
public class Vitality : OutsourceModifier<Healing, HealthPoint>
{
    [Range(0, 1)][SerializeField] private float increasingValue;

    internal override void OnModified(Amount currentAmount, IUnitStat unit)
    {
        currentAmount += (increasingValue);
    }

    internal override void OnExpired(Amount currentAmount, IUnitStat unit)
    {
        currentAmount += (-increasingValue);
    }
}

