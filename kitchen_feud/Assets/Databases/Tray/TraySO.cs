using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tray", menuName = "Assets/Tray")]

public class TraySO : ScriptableObject
{
    public string trayID;
    public List<BaseFood> ServingTray = new List<BaseFood>();
}
   