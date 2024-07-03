using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Hung/PointerDownEvent/PopDown")]
public class Pop : PointerDownEvent
{
    [SerializeField] private float scale;
    public override void Cast(SimpleButton caller, PointerEventData eventData)
    {
        caller.eventTarget.localScale *= scale;
    }

    public override void Recast(SimpleButton caller, PointerEventData eventData)
    {
        caller.eventTarget.localScale /= scale;
    }
}
