using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Pantry", menuName ="Pantry System/Pantry")]
public class PantryObject : ScriptableObject
{
    public List<PantrySlot> Container = new List<PantrySlot>();

    public void AddItem( ItemObject _item, int _amount){
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
            Container.Add(new PantrySlot(_item, _amount));
        }
    }
}

[System.Serializable]
public class PantrySlot{
    public ItemObject item;
    public int amount;
    public PantrySlot(ItemObject _item, int _amount){
        item = _item;
        amount = _amount;
    }
    public void AddAmount(int value){
        amount += value;
    }
}