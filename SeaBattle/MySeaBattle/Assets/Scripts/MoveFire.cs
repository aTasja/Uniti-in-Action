using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveFire : MonoBehaviour {

    public float speed;
    MoveAim aim;
    
    public void Update () {

        Vector2 temp = aim.getAimPos();
        
        transform.position = Vector3.MoveTowards(transform.position, temp, Time.deltaTime * speed);
       
    }
}
