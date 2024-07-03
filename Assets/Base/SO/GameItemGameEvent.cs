using UnityEngine;

namespace ScriptableObjectArchitecture
{
    [System.Serializable]
    [CreateAssetMenu(
        fileName = "GameItemGameEvent.asset",
        menuName = SOArchitecture_Utility.GAME_EVENT + "Game Item",
        order = SOArchitecture_Utility.ASSET_MENU_ORDER_EVENTS + 0)]
    public sealed class GameItemGameEvent : GameEventBase<GameItem>
    {
    }
}