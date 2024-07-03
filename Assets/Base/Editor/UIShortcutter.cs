using UnityEngine;
using UnityEditor;
using System;
using UnityEditor.AI;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

public class UIShortcutter: Editor
{
    [MenuItem("Jobs/Resolve and Play &s")]
    static public async void ResolveAndPlay()
    {
        if (!EditorApplication.isPlaying)
        {
            try
            {
            SingletonRole.GameFlow.OnReset();
            Type type = SingletonRole.GameFlow.GetType();
            PrefabUtility.RecordPrefabInstancePropertyModifications(SingletonRole.GameFlow.gameObject.GetComponent(type));
            
            Test.Instance.OnReset();
            PrefabUtility.RecordPrefabInstancePropertyModifications(Test.Instance);

            await Task.Delay(200);

            }
            catch
            {

            }
            EditorApplication.EnterPlaymode();
        }
        else
        {
            EditorApplication.ExitPlaymode();
        }      
    }

    static IModel modelHolder;
    [MenuItem("GameObject/Toggle Active &a")]
    static public void ToggleActive()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            modelHolder = obj.GetComponentInParent<IModel>();
            if (modelHolder != null)
            {
                if (modelHolder.Model == obj && modelHolder.gameObject != obj)
                {
                    if (!obj.activeSelf) obj.SetActive(true);
                    //Debug.Log("LuL"); 
                    return;
                }
            }

            if (obj.GetComponent<ISingletonRole>() != null)
            {
                //Debug.Log("Kekw"); 
                obj.SetActive(true);
                
            }
            else
            {
                Undo.RegisterCompleteObjectUndo(obj, "kekw");
                //Debug.Log(obj.activeSelf);
                obj.SetActive(!obj.activeSelf);
                
            }
        }
    }
        
    [MenuItem("GameObject/Reset #r")]
    static public void Reset()
    {
        bool involved;
        UnityEngine.Object selectedObject = Selection.activeObject;
        if (selectedObject is GameObject)
        {
            GameObject gameObj = (GameObject)selectedObject;
            involved = false;
            foreach (IReset resetter in gameObj.GetComponents<IReset>())
            {
                involved = true;
                resetter.OnReset();
                Type type = resetter.GetType();               
                PrefabUtility.RecordPrefabInstancePropertyModifications(gameObj.GetComponent(type));
                Undo.RegisterCompleteObjectUndo(gameObj, "Reset data");
            }

            if (!involved)
            {
                if (gameObj.tag == "ResetOnParent")
                {
                    IReset resetter = gameObj.GetComponentInParent<IReset>();
                    resetter.OnReset();
                    Type type = resetter.GetType();
                    PrefabUtility.RecordPrefabInstancePropertyModifications(gameObj.GetComponentInParent(type));
                }
                else
                {
                    Transform trx = gameObj.transform;
                    Undo.RegisterCompleteObjectUndo(trx, "Reset game object to origin");

                    trx.localPosition = Vector3.zero;
                    trx.localRotation = Quaternion.identity;
                    trx.localScale = Vector3.one;
                }
            }
            
        }
        else if (selectedObject is IReset)
        {
            ((IReset)selectedObject).OnReset();
        }                
    }

    [MenuItem("GameObject/target Transformizable #w")]

    static public void TargetTransformizable()
    {
        GameObject target = Selection.gameObjects[0].GetComponentInParent<ITransformizable>()?.gameObject;
        if (target == null)
        {
            target = FindParentWithTransformizableTag(Selection.gameObjects[0].transform);
        }
        if (target != null) Selection.SetActiveObjectWithContext(target, null);
    }

    static GameObject FindParentWithTransformizableTag(Transform current)
    {
        if (current != null)
        {
            if (current.tag == "Transformizable") return current.gameObject;
            else return FindParentWithTransformizableTag(current.parent);
        }           
        else return null;
    }

    [MenuItem("GameObject/Test Action #t")]

    static public void TestAction()
    {
        Selection.gameObjects[0].GetComponent<ITest>().OnTest();

    }

    [MenuItem("GameObject/Gizmos Toggle #g")]
    static public void GizmosToggle()
    {
        IGizmos gizmos;
        foreach (GameObject obj in Selection.gameObjects)
        {
            gizmos = obj.GetComponent<IGizmos>();
            if (gizmos != null)
            {
                gizmos.debugMode = !gizmos.debugMode;
            }
        }      
    }


    [MenuItem("GameObject/target Player #p")]
    static public void TargetPlayer()
    {
        Selection.SetActiveObjectWithContext(SingletonRole.Player.gameObject, null);
    }

    [MenuItem("GameObject/target Main Camera #c")]
    static public void TargetMainCamera()
    {
        Selection.SetActiveObjectWithContext(SingletonRole.Cameraman.gameObject, null);
    }

    [MenuItem("GameObject/target Game Flow #f")]
    static public void TargetGameFlow()
    {
        Selection.SetActiveObjectWithContext(SingletonRole.GameFlow.gameObject, null);
    }



    //[MenuItem("Map Generator/Change Map To City #1")]
    //static public void ChangeMapToCity()
    //{
    //    ChangeMapEditor(MapName.City);
    //}

    //[MenuItem("Map Generator/Change Map To Beach #3")]
    //static public void ChangeMapToBeach()
    //{
    //    ChangeMapEditor(MapName.Beach);
    //}

    //private static Map _map;

    //private static void ChangeMapEditor(MapName mapName)
    //{
    //    Map newMapPrefab = MapManager.Instance.LoadMap(mapName, out Map oldMap, out Action<Map> createMapAction);

    //    if (oldMap)
    //    {
    //        PrefabUtility.ApplyPrefabInstance(oldMap.gameObject, InteractionMode.AutomatedAction);
    //        //DestroyImmediate(oldMap.gameObject);
    //        //MapManager.Instance.UpdateData(oldMap.Name);
    //        oldMap.gameObject.SetActive(false);
    //    }

    //    if (!_map)
    //    {
    //        _map = (Map)PrefabUtility.InstantiatePrefab(newMapPrefab);            
    //    }
    //    else
    //    {
    //        _map.gameObject.SetActive(true);
    //    }
    //    createMapAction(_map);
    //    _map = oldMap;
    //}

    //[MenuItem("Map Generator/Remove All Map Datas")]
    //static public void RemoveAllMapDatas()
    //{
    //    MapManager.Instance.RemoveAllDatas();
    //}
}