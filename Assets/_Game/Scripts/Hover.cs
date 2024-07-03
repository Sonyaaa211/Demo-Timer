using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    public GameObject hoverObject;

    public void OnMouseOver()
    {
        hoverObject.SetActive(true);
    }
    public void OnMouseExit()
    {
        hoverObject.SetActive(false);
    }
}
