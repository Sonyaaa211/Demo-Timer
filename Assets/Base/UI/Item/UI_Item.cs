using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class UI_ItemBase: UIBehaviour
{

}

public abstract class UI_Item : UI_ItemBase, IPickable, ITransformizable
{
    RectTransform _rect;
    public RectTransform rect
    {
        get
        {
            if (_rect != null) return _rect;
            else
            {
                _rect = (RectTransform)transform;
                return _rect;
            }
        }
    }

    public abstract void OnPicked();

    public abstract void OnUnpicked();
}

public abstract class UI_StaticItem<T> : UI_ItemBase, IPickable, ITransformizable, IDataVisualize<T> where T : IIdentifiedData
{
    [field: SerializeField] public T info { get; private set; }

    protected override void Awake()
    {
        Set(info, OwnedState.Available);
    }

    public virtual void Set(T info, OwnedState ownedState)
    {
        this.info = info;
    }

    public abstract void OnPicked();

    public abstract void OnUnpicked();
}

public abstract class UI_ListItem<T> : UI_Item, IDataVisualize<T> where T: IIdentifiedData
{
    [field:ReadOnly] [field: SerializeField] public T info { get; private set; }

    public virtual void Set(T info, OwnedState ownedState)
    {
        this.info = info;
        if (this is INoti noti && noti.isShowingNotification)
        {

        }
    }
}

public enum OwnedState
{
    Lock,
    Available,
    Owned
}

public interface INoti
{
    bool isShowingNotification { get; }
}