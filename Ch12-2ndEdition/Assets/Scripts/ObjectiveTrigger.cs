using UnityEngine;
using System.Collections;
public class ObjectiveTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //только игрок может переходить на следующий уровень
        //но если у enemy убрать компнент rigid body,
        //нужный для попаданая снарядов в него, то проверять коллаидер на то что он игрока не нужно
        if (other.gameObject.name == "player") 
        {
            Managers.Mission.ReachObjective();
        }
    }
}
