using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Hung/PointerDownEvent/Disappear")]
public class Disappear : PointerDownEvent
{
    [SerializeField] private float scale;
    public override void Cast(SimpleButton caller, PointerEventData eventData)
    {
        caller.eventTarget.localScale *= scale;
    }

    public override void Recast(SimpleButton caller, PointerEventData eventData)
    {
        caller.gameObject.SetActive(false);
        caller.OnPointerClick(null);
    }
}
