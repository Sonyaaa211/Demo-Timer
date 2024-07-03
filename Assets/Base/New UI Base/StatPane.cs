using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatPane : MonoBehaviour
{
    [SerializeField] private IStatHolderGameEvent visualDataChange;
    //[SerializeField] private BindingStat bindingStat;

    [SerializeField] private Image iconStat;
    [SerializeField] private TMP_Text textStatName;
    [SerializeField] private TMP_Text textStatValue;

    private void OnEnable()
    {
        visualDataChange.AddListener(OnDataChange);
    }
    
    private void OnDisable()
    {
        visualDataChange.RemoveListener(OnDataChange);
    }

    private void Awake()
    {
        
    }

    private void OnDataChange(IStatHolder statHolder)
    {

    }
}
