using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Menu Database", menuName = "Assets/Databases/Menu Database")]
public class MenuDatabase : ScriptableObject
{
    public List<MenuSO> allMenus;

}

