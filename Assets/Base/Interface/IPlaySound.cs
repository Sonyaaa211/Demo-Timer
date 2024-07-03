using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISettingOptionToggler<T> where T : ISettingOption
{
    void Register(T player);

    void Remove(T player);
}

public interface IPlaySound<T>: IPlaySound where T: System.Enum
{
    void PlaySound(T soundType);
}

public interface ISettingOption
{

}

public interface IPlaySound: ISettingOption
{
    void ToggleSound(bool isOn);
}

public interface IPlayShake : ISettingOption
{
    void ToggleShake(bool isOn);
}