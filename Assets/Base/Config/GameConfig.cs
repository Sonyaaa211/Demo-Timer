using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Hung/Config/Game Config")]
public partial class GameConfig : SOSingleton<GameConfig>
{
    [field:Range(0,10)][field:SerializeField] public float timeScale { get; set; }
}
