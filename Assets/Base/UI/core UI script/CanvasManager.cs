using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [field: SerializeField] public Canvas UICanvas { get; private set; }
}
