using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeviceOperator : MonoBehaviour {

    public float radius = 1.5f; // расстояние, с кторого персонаж может активировать устройства
    
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire3"))
        {
            Debug.Log("SHIFT BUTTON PRESSED");
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius); // метод OverlapSphere возвращает список ближайших объектов
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > .5f)
                {
                    hitCollider.SendMessage("Operate", SendMessageOptions.DontRequireReceiver); // метод SendMessage пытается вызвать Operate в не зависимости от типа целевого объекта
                }
            }
        }
	}




}
