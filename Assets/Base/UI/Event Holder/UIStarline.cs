using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public class UIStarline : UI_EventHolder
{
    [SerializeField] private int _pickedNumber;
    [SerializeField] private Image[] stars;

    private void Awake()
    {
        stars = GetComponentsInChildren<Image>().Where(image => image.gameObject.tag == "Toggle").ToArray();
    }

    int _index;
    int i;
    protected override bool ClickAction(PointerEventData eventData, GameObject clicked)
    {
        _index = clicked.transform.GetSiblingIndex();

        for(i = 0; i <= _index; i++)
        {
            SetStar(i, true);
        }
        for (i = _index + 1; i < _pickedNumber; i++)
        {
            SetStar(i, false);
        }
        _pickedNumber = _index + 1;
        return true;
    }

    private void SetStar(int index, bool set)
    {
        stars[index].gameObject.SetActive(set);
    }
}
