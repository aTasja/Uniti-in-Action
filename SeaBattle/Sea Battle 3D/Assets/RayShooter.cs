using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour {

    private Camera _camera;
    
    private Texture2D cursorTexture;

    // Use this for initialization
    void Start () {
        _camera = GetComponent<Camera>();

        // скрывает курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        cursorTexture = (Texture2D)Resources.Load("Textures/smallAim");
        
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) //реакция на нажатие кнопки мыши
        {
            
            //середина экрана это половина его ширины и высоты
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);  
            //создание в этой точке луча
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;//инфа о пересечении лучаб о точке где возник луч и об объекте с кот.столкнулся
            //испущенный луч заполняет информацией переменную, на которую имеется ссылка
            if(Physics.Raycast(ray, out hit)) //возвращает true если луч столкнулся
            {
                Debug.Log("Hit " + hit.point); //загружаем коордтнаты точки, в которую попал луч
                
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    //Debug.Log("Target hit");
                    target.ReactToHit();

                }
                else
                    
                    StartCoroutine(SphereIndicator(hit.point));
            }
        }
	}

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        sphere.transform.position = pos;
        // задаем цвет сферы
        sphere.GetComponent<Renderer>().material.color = Color.green;

        //ключевое слово yield указывает сопрограмме когда следует остановиться
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    void OnGUI()
    {
        float posX = _camera.pixelWidth / 2 - cursorTexture.width / 2;
        float posY = _camera.pixelHeight / 2 - cursorTexture.height / 2;
        GUI.DrawTexture(new Rect(posX, posY, cursorTexture.width, cursorTexture.height), cursorTexture);
    }

}
