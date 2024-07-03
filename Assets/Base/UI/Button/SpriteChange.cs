using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName ="Hung/PointerDownEvent/SpriteChange")]
public class SpriteChange : PointerDownEvent
{

    public override void Cast(SimpleButton caller, PointerEventData eventData)
    {
        caller.eventTarget.gameObject.SetActive(true);
    }

    public override void Recast(SimpleButton caller, PointerEventData eventData)
    {
        caller.eventTarget.gameObject.SetActive(false);
    }
}
