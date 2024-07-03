using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameItem : SerializedScriptableObject, IIdentifiedData, IPurchasable
{
    [field: SerializeField] public int ID { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Sprite Icon { get; private set; }

    [field:TextArea] [field:SerializeField] public string Description { get; private set; }

    public virtual void OnPurchased(int amount)
    {
        Debug.Log("Purchase " + name + " for " + amount + " amounts"); 
    }
}
