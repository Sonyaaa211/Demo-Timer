using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PointerDownEvent : ScriptableObject
{
    public abstract void Cast(SimpleButton caller, PointerEventData eventData);

    public abstract void Recast(SimpleButton caller, PointerEventData eventData);
}
