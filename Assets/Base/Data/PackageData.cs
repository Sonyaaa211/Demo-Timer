using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hung/Data/Package")]
public class PackageData : GameItem
{
    [field:SerializeField] public GameObject Visual { get; private set; }

    [field:SerializeField] public List<GameItemAmount> Items { get; private set; }
}

[Serializable]
public struct GameItemAmount
{
    public GameItem item;
    public int amount;
}
