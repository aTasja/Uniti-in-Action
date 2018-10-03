using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    [SerializeField] private string itemName;

	void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item collected: " + itemName);
        //только игрок может собирать русурсы, но если у enemy убрать компнент rigid body, 
        //нужный для попаданая снарядов в него, то проверять коллаидер на то что он игрока не нужно
        if (other.gameObject.name == "player") 
        {
            Managers.Inventory.AddItem(itemName);
            Destroy(this.gameObject);
        }
    }
}
