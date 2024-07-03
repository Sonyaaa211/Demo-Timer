using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public interface IKeyNumberListener: IMono
{
    void OnKeyNumberPress(int index);
}

public class Test : Singleton<Test>, IReset
{
    public delegate void Action();

    private static Action testAction, testAction1, testAction2, testAction3;
    private static Action<bool> signalAction;

    [SerializeField] internal bool signal;

    public void SwapSignal()
    {
        signal = !signal;
    }

    public void Test_Function(Vector2 input)
    {
        //Debug.Log(input); 
    }

    public void Test_Function2(Vector3 input)
    {
        Debug.Log(input);
    }

    private void Start()
    {
        //T(() => WeaponController.Instance.BulletEndingScenarioCall());
        //G(() => CameraicFollower.Instance.MainCamCall());
        //T(() => testingObj.transform.LookAt(Vector3.zero));
        //T(() => testingObj.GetComponent<Bullet>().Cast1());
        //CastFor<Building>();

        //T(() =>
        //{
        //    MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        //    CombineInstance[] combine = new CombineInstance[meshFilters.Length];

        //    int i = 0;
        //    while (i < meshFilters.Length)
        //    {
        //        combine[i].mesh = meshFilters[i].sharedMesh;
        //        combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
        //        meshFilters[i].gameObject.SetActive(false);

        //        i++;
        //    }
        //    Transform transform = testingObj.transform;
        //    transform.GetComponent<MeshFilter>().mesh = new Mesh();
        //    transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        //    transform.gameObject.SetActive(true);        
        //});

        //CastFor<Throw_Ability>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Player.instance.UsePowerUp(PowerUp.RandomWeapon);
            if (testAction != null)
            {
                testAction();
                Debug.Log("T has been actually pressed to cast " +  testAction.GetInvocationList().Length + " actions !");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            testAction1();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            testAction2();
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            testAction3();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            if (signalAction != null) signalAction(signal);
            signal = !signal;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            //WeaponController.Instance.Click_Reload();
        }
        else
        {
            for (int i = (int) KeyCode.Alpha1; i <= (int)KeyCode.Alpha8; i++)
            {
                if (Input.GetKeyDown((KeyCode)i))
                {
                    keyListeners.ForEach(listener => listener.GetComponent<IKeyNumberListener>().OnKeyNumberPress(i - (int)KeyCode.Alpha1));
                }
            }           
        }
#endif 
    }

    [SerializeField] List<GameObject> keyListeners;

    public void OnReset()
    {
        keyListeners = TypeFinder.FindGameObjectsOfComponent<IKeyNumberListener>();
    }

    public static void T_Only(Action action)
    {
        testAction = action;
    }

    public static void T_Minus(Action action) { testAction -= action; }

    public static void T(Action action, bool alsoDoAction = false)
    {
        testAction += action;
        if (alsoDoAction)
        {
            action();
        }
    }

    public static void F(Action action)
    {
        testAction1 = action;
    }

    public static void G(Action action)
    {
        testAction2 += action;
    }

    public static void J(Action action)
    {
        testAction3 += action;
    }

    public static void Signal(Action<bool> action)
    {
        signalAction += action;
    }
}

public interface ITest
{
    void OnTest();
}