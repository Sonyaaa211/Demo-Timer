using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "IStatHolderGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "Stat Holder",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 1)]
    public sealed class IStatHolderGameEvent : GameEventBase<IStatHolder>
    {
    }
}