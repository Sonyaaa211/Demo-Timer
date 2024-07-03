using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPersistentData
{
    private const string PREFIX_OwnedState = "OwnedState-";
    private const string PREFIX_Skin = "Skin";

    private static int GetIntFromList(string key, int index, int firstGivenNumber = 1)
    {
        string[] ss = PlayerPrefs.GetString(key, firstGivenNumber + "-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0").Split('-');
        return int.Parse(ss[index]);
    }

    private static void SetIntToList(string key, int index, int changedValue, int firstGivenNumber = 1)
    {
        string[] ss = PlayerPrefs.GetString(key, firstGivenNumber + "-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0-0").Split('-');
        ss[index] = changedValue.ToString();
        PlayerPrefs.SetString(key, string.Join("-", ss));
    }

    public static Switch Get_SettingOption(SettingOption settingOption)
    {
        return (Switch)PlayerPrefs.GetInt(settingOption.ToString(), 1);
    }

    public static void Set_SettingOption(SettingOption settingOption, Switch set)
    {
        PlayerPrefs.SetInt(settingOption.ToString(), (int) set);
    }

    public static int MaxLevel
    {
        get => PlayerPrefs.GetInt(nameof(MaxLevel), 0);

        set => PlayerPrefs.SetInt(nameof(MaxLevel), value);
    }

    public static OwnedState Get_OwnedStateSkinShop(int id)
    {
        return (OwnedState)GetIntFromList(PREFIX_OwnedState + PREFIX_Skin, id, 2);
    }

    public static void Set_OwnedStateSkinShop(int id, OwnedState ownedState)
    {
        SetIntToList(PREFIX_OwnedState + PREFIX_Skin, id, (int) ownedState, 2);
    }

    public static int Skin
    {
        get => PlayerPrefs.GetInt(PREFIX_Skin, 0);

        set => PlayerPrefs.SetInt(PREFIX_Skin, value);
    }
}
