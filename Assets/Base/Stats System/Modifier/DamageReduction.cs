using Hung.StatSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hung/Source Modifier/Outsource/Damage/Damage Reduction")]
public class DamageReduction : OutsourceModifier<Damage, HealthPoint>
{
    [Range(0, 1)][SerializeField] private float reductionValue;

    internal override void OnExpired(Amount currentAmount, IUnitStat unit)
    {
        currentAmount *=(1 / (1 - reductionValue));
    }

    internal override void OnModified(Amount currentAmount, IUnitStat unit)
    {
        currentAmount *= (1 - reductionValue);
    }
}
