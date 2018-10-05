using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    [SerializeField] private string itemName;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip grabSound;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Item collected: " + itemName);
        //только игрок может собирать русурсы, но если у enemy убрать компнент rigid body, 
        //нужный для попаданая снарядов в него, то проверять коллаидер на то что он игрока не нужно
        if (other.gameObject.name == "player") 
        {
            audioSource.PlayOneShot(grabSound);
            Managers.Inventory.AddItem(itemName);
            Destroy(this.gameObject);
        }
    }
}
