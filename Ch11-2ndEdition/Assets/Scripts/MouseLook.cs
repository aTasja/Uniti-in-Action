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

    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY=2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;

    // скорость вращения в горизонтальной плоскости
    public float sensitivityHor = 2.0f;
    
    // скорость вращения в вертикальной плоскости
    public float sensitivityVert = 2.0f;
    // макс и мин углы наклона в вертикальной плоскости
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    private float _rotationX = 0; //закрытая переменная для угла по вертикали

    void Start()
    {
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) //проверяем существует ли компонент
            body.freezeRotation = true;
    }

    // Update is called once per frame
    void Update () {
		if (axes == RotationAxes.MouseX)
        {
            //это поворот в горизонтальной плоскости
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);

        }
        else if (axes == RotationAxes.MouseY)
        {
            //это поворот в вертикальной плоскости
            // увеличиваем угол поворота по верт. в соотв. с перемещениями мыши
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            //фиксируем угол поворота по верт. в соотв. с заданными макс и мин
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            //сохраняем одинаковый угол поворота вокруг оси Y (врашение в гориз. плоскости нет)
            //float rotationY = transform.localEulerAngles.y;
            //создаем новый вектор из сохраненных значений поворота
            //transform.localRotation = new Quaternion(_rotationX, rotationY, 0, 0); //качает головой
            transform.localEulerAngles = new Vector3(_rotationX, transform.localEulerAngles.y, 0); // кивает головой
      
        }
        else
        {
            // это комбинированный поворот
            
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            //приращение угла поворота через значение delta -  величину изменения угла поворота
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y + delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            //Debug.Log("+++++++++++++++comby+++++++++++++++++");

        }
        
	}
}
