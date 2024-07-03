using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class GameplayFlow : MonoBehaviour, IGameplayFlow
{
    [field: SerializeField] public GameStateInfo stateInfo { get; private set; }

    [field: SerializeField] public List<GameObject> GameplayLoaders { get; private set; }

    [field:SerializeField] public List<GameObject> GameplayPrepareStarters {get; private set; }

    [field: SerializeField] public List<GameObject> GameplayStarters { get; private set; }

    public List<GameObject> GameplayLoopers { get; private set; }

    [field:SerializeField] public List<GameObject> GameplayPrepareEnders { get; private set; }

    [field:SerializeField] public List<GameObject> GameplayEnders { get; private set; }

    [field: SerializeField] public List<GameObject> GameplayInterupters { get; private set; }


    public void OnReset()
    {
        GameplayLoaders = TypeFinder.FindGameObjectsOfComponent<IGameplayLoad>().Select(starter => starter.gameObject).Where(starter => starter.gameObject != gameObject).ToList();

        GameplayPrepareStarters = TypeFinder.FindGameObjectsOfComponent<IGameplayPrepareStart>().Select(starter => starter.gameObject).Where(starter => starter.gameObject != gameObject).ToList();

        GameplayStarters = TypeFinder.FindGameObjectsOfComponent<IGameplayStart>().Where(i => i.gameObject != gameObject).Select(i => i.gameObject).ToList();

        GameplayPrepareEnders = TypeFinder.FindGameObjectsOfComponent<IGameplayPrepareEnd>().Select(starter => starter.gameObject).Where(starter => starter.gameObject != gameObject).ToList();

        GameplayEnders = TypeFinder.FindGameObjectsOfComponent<IGameplayEnd>().Where(i => i.gameObject != gameObject).Select(i => i.gameObject).ToList();

        GameplayInterupters = TypeFinder.FindGameObjectsOfComponent<IGameplayInterupt>().Where(i => i.gameObject != gameObject).Select(i => i.gameObject).ToList();
    }

    private void Start()
    {
        stateInfo = new(currentState: GameState.Win);
    }

    public void OnGameplayLoad()
    {
        GameplayLoaders.ForEach(g =>
        {
            foreach(IGameplayLoad i in g.GetComponents<IGameplayLoad>()) i.OnGameplayLoad();
        });
    }

    public void OnGameplayPrepareStart()
    {
        stateInfo = new(currentState: GameState.OnGoing);
        GameplayPrepareStarters.ForEach(g => 
        {
            foreach (IGameplayPrepareStart i in g.GetComponents<IGameplayPrepareStart>()) i.OnGameplayPrepareStart();
        });
    }

    public void OnGameplayStart()
    {
        GameplayStarters.ForEach(g =>
        {
            foreach (IGameplayStart i in g.GetComponents<IGameplayStart>()) i.OnGameplayStart();
        });
    }

    public void OnGameplayLoop()
    {
        
    }

    public async void OnGameplayPrepareEnd(GameStateInfo stateInfo)
    {
        if (!isAlreadyEnd)
        {
            isAlreadyEnd = true;
            this.stateInfo = stateInfo;
            GameplayPrepareEnders.ForEach(g =>
            {
                foreach (IGameplayPrepareEnd i in g.GetComponents<IGameplayPrepareEnd>()) i.OnGameplayPrepareEnd(stateInfo);
            });

            await Task.Delay(1800);

            OnGameplayEnd(stateInfo);
        }       
    }


    private bool isAlreadyEnd;   
    public void OnGameplayEnd(GameStateInfo stateInfo)
    {
        this.stateInfo = stateInfo;
        GameplayEnders.ForEach(g =>
        {
            foreach (IGameplayEnd i in g.GetComponents<IGameplayEnd>()) i.OnGameplayEnd(stateInfo);
        });
    }

    public void OnGameplayInterupt()
    {
        GameplayInterupters.ForEach(g =>
        {
            foreach (IGameplayInterupt i in g.GetComponents<IGameplayInterupt>()) i.OnGameplayInterupt();
        });
    }
}
