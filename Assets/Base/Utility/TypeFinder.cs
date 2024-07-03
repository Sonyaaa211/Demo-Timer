using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class TypeFinder: Object
{
    public static List<T> FindMultiComponents<T>(bool isIncludeInactive = false)
    {
        return FindObjectsOfType<MonoBehaviour>(isIncludeInactive).OfType<T>().ToList();
    }
    
    public static List<GameObject> FindGameObjectsOfComponent<T>() where T: IMono
    {
        IEnumerable<T> inters = FindObjectsOfType<MonoBehaviour>().OfType<T>();
        HashSet<GameObject> set = new HashSet<GameObject>();

        foreach (T inter in inters)
        {
            set.Add(inter.gameObject);
        }

        return set.ToList();
    }
}
