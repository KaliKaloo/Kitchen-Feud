using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Subroom", menuName ="Subroom System/Subroom")]
public class SubroomObject : ScriptableObject
{
    public List<SubroomSlot> Container = new List<SubroomSlot>();

    public void AddItem( BaseFood _item, int _amount){
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].item == _item){
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        }
        if(!hasItem){
            Container.Add(new SubroomSlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class SubroomSlot{
    public BaseFood item;
    public int amount;
    public SubroomSlot(BaseFood _item, int _amount){
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }
}