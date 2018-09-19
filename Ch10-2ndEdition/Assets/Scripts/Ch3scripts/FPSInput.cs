using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// basic WASD-style movement control
// commented out line demonstrates that transform.
//Translate instead of charController.Move doesn't have collision detection

[RequireComponent(typeof(CharacterController))]
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour {

    public float speed = 0.03f;
    public float gravity = -29.8f;

    private CharacterController _characterController;
    
    // Use this for initialization
	void Start () {
        _characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        //transform.Translate(deltaX * Time.deltaTime, 0, deltaZ * Time.deltaTime);
        Vector3 movement = new Vector3(deltaX, 0, deltaZ);

        //ограничение движения по диагонали той же скоростью, что и движение парралельно осям
        movement = Vector3.ClampMagnitude(movement, speed);

        movement.y = gravity; //добавили гравитацию

        movement *= Time.deltaTime; //огрпничили скорость перемещения

        //преобразуем вектор движения от локальных к глобальным координатам
        movement = transform.TransformDirection(movement);

        //заставим этот вектор перемещать компонент CharacterController 
        _characterController.Move(movement);
	}
}
