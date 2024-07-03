using Hung.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hung/Source Modifier/Outsource/Damage/Vulnerable")]
public class Vulnerable : OutsourceModifier<Damage, HealthPoint>
{
    [Range(0, 1)][SerializeField] private float inscreasedValue;

    internal override void OnExpired(Amount currentAmount, IUnitStat unit)
    {
        currentAmount *= (1 / (1 + inscreasedValue));
    }

    internal override void OnModified(Amount currentAmount, IUnitStat unit)
    {
        currentAmount *= (1 + inscreasedValue);
    }
}
