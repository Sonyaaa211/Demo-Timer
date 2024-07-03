using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector<T>: MonoBehaviour where T: MonoBehaviour
{
    [SerializeField] protected Collider collider;
    public event Action<T> OnTrigger;

    T _detected;
    private void OnTriggerEnter(Collider other)
    {
        _detected = other.GetComponentInParent<T>(); 
        if (_detected != null && OnTrigger != null)
        {
            OnTrigger(_detected);
            //if (GameConfig.Instance.detectorDebugMode) Debug.Log(name + " has detected " + LayerMask.LayerToName(other.gameObject.layer));
        }       
    }
}

public class Detector : MonoBehaviour
{
    [SerializeField] protected Collider collider;

    public event Action OnTrigger;

    private void OnTriggerEnter(Collider other)
    {
        OnTrigger();
    }
}
