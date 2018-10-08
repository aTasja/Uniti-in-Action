using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MouseLook rotates the transform based on the mouse delta.
// To make an FPS style character:
// - Create a capsule.
// - Add the MouseLook script to the capsule.
//   -> Set the mouse look to use MouseX. (You want to only turn character but not tilt it)
// - Add FPSInput script to the capsule
//   -> A CharacterController component will be automatically added.
//
// - Create a camera. Make the camera a child of the capsule. Position in the head and reset the rotation.
// - Add a MouseLook script to the camera.
//   -> Set the mouse look to use MouseY. (You want the camera to tilt up and down like a head. The character already turns.)


public class MouseLook : MonoBehaviour {

    // скорость вращения в горизонтальной плоскости
    public float sensitivityHor = 20.0f;

    // макс и мин углы наклона в горизонтальной плоскости
    public float minimumHor = -45.0f;
    public float maximumHor = 45.0f;

    //закрытая переменная для угла по горизонтали
    private float _rotationY = 0;

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) //проверяем существует ли компонент
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update () {

        _rotationY += Input.GetAxis("Mouse X") * sensitivityHor; //* Time.deltaTime;
        _rotationY = Mathf.Clamp(_rotationY, -45, 45);
 
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, _rotationY, 0);

        
       
        
	}
}
