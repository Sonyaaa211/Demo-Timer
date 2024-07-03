using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorldTextType
{
    Damage,
    Heal,
    CriticalDamage,
    SmallDamage
}

public class TextSpawner : Singleton<TextSpawner>
{
    //[SerializeField] private DamageNumber[] origins;

    public void Spawn(Vector3 position, float value, WorldTextType textType = WorldTextType.Damage)
    {
        value = Mathf.Abs(value);
        //Debug.Log("Damage: " + value); 
        if (value >= 0.5f)
        {
            //origins[(int)textType].Spawn(position, Mathf.RoundToInt(value));
        }       
    }
}

