using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IConfig
{

}

public interface IConfigurable<T> where T : IConfig
{
    T config { get; }
}
