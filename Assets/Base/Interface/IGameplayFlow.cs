using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public interface IGameplayFlow: IFlow, ISingletonRole, IGameplayLoad, IGameplayPrepareStart, IGameplayStart, IGameplayLoop, IGameplayPrepareEnd, IGameplayEnd, IGameplayInterupt, IReset                              
{
    GameStateInfo stateInfo { get; }
}

public interface IGameplayLoad: IMono
{
    void OnGameplayLoad();
}

public interface IGameplayPrepareStart: IMono
{
    void OnGameplayPrepareStart();
}

public interface IGameplayStart: IMono
{
    void OnGameplayStart();
}

public interface IGameplayLoop: IMono
{
    void OnGameplayLoop();
}

public interface IGameplayPrepareEnd: IMono
{
    void OnGameplayPrepareEnd(GameStateInfo stateInfo);
}

public interface IGameplayEnd: IMono
{
    void OnGameplayEnd(GameStateInfo stateInfo);
}

public interface IGameplayInterupt: IMono
{
    void OnGameplayInterupt();
}

public interface IGameStateInfo
{
    
}