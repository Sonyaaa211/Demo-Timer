using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RoleClass: byte
{
    Neutral,
    Warrior,
    Mage,
    Archer,
    Necromancer,
    Assassin
}

public enum BindingStat: byte
{
    ATK,
    HP,
    Armor,
    Range,
    MS,
    Evasion,
    CritChance,
    CritDMG
}

public interface IRoleClass
{
    RoleClass Class { get; }
}

public interface IStatHolder
{
    float TryToGetValue(BindingStat stat);
}


public class CharacterData : SOSingleton<CharacterData>
{

}
