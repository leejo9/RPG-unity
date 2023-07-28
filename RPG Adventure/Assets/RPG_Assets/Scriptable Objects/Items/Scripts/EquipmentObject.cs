using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Object", menuName = "Inventory System/Items/Equipment")]

public class EquipmentObject : ItemObject
{

    public void Awake()
    {
        type = ItemType.Chest;
    }
}
