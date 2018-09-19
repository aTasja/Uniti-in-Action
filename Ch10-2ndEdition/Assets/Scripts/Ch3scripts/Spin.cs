using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float speed = 1.0f;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, speed, 0);
	}
}
