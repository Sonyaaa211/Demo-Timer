using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IIdentifiedData
{
    int ID { get; }

    string Name { get; }

    string Description { get; }
}

public interface IPurchasable
{
    void OnPurchased(int amount);
}