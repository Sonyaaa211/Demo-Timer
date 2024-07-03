
using UnityEngine;

public class Singleton<T> : MonoBehaviour, ISingletonRole where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(T)) as T;
                if (instance == null) Debug.Log("Turn on the " + typeof(T).ToString() + "!");
            }
            return instance;
        }
    }
}

