using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

    //скорость движения и расстояние с кот. начинается распознавание препятствия
    public float speed = 3.0f;
    public float obstracleRange = 5.0f;

    private bool _alive;

    [SerializeField] private GameObject fireballPrefab;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip hitSound;

    private GameObject _fireball;

	// Use this for initialization
	void Start () {
        _alive = true;
        
        //Отключаем влияние среды на врага (чтобы сам не падал)
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null) //проверяем существует ли компонент
            body.freezeRotation = true;
    }
	
	// Update is called once per frame
	void Update () {
        // enemy постоянно движется вперед если жив
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
            //луч нацеливается туда куда движется цель
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            //бросаем луч с описанной вокруг него окружностью
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {

                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    _fireball = Instantiate(fireballPrefab) as GameObject;
                    //поместим огненный шар перед врагом и нацелим его в направлении его движения
                    _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);

                    audioSource.PlayOneShot(hitSound);
                    
                    _fireball.transform.rotation = transform.rotation;
                }

                else if (hit.distance < obstracleRange)
                {
                    float angle = Random.Range(-110, 100); //поворачиваем в наполовину случайном направлении
                    transform.Rotate(0, angle, 0);

                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
