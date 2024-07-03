using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ref: Singleton<Ref>
{
    [field: SerializeField] public Canvas WorldCanvas { get; private set; }

}
