using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIPackage : UI_StaticItem<PackageData>, IReset
{
    [SerializeField] private TMP_Text textName;
    [SerializeField] private TMP_Text textDes;
    [SerializeField] private Transform visualHolder;

    [SerializeField] private List<UIAmountItem> purchaseItems;

    public override void OnPicked()
    {
        throw new System.NotImplementedException();
    }

    public void OnReset()
    {
        purchaseItems = GetComponentsInChildren<UIAmountItem>().ToList();    
    }

    public override void OnUnpicked()
    {
        throw new System.NotImplementedException();
    }

    public override void Set(PackageData info, OwnedState ownedState)
    {
        base.Set(info, ownedState);

        textName.text = info.Name;
        textDes.text = info.Description;
        Instantiate(info.Visual, visualHolder);

        for (int i = 0; i < purchaseItems.Count; i++)
        {
            purchaseItems[i].SetInfo(info.Items[i].item, info.Items[i].amount);
        }
    }
}
