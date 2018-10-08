using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour {

    //скорость движения и расстояние с кот. начинается распознавание препятствия
    public float speed = 3.0f;
    
    public float minX = -38.0f;
    public float maxX = 38.0f;

    private bool _alive;
    private int _direction = -1;


    [SerializeField] private GameObject fireballPrefab;
    private GameObject _fireball;

	// Use this for initialization
	void Start () {
        _alive = true;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        // ship постоянно движется вперед если жив
        if (_alive)
        {
            

            transform.Translate(_direction * speed * Time.deltaTime, 0, 0);

            bool bounced = false;
            if (transform.position.x > maxX || transform.position.x < minX)
            {
                _direction = -_direction;
                bounced = true;
            }
            if (bounced)
            {
                transform.Translate(_direction * speed * Time.deltaTime, 0, 0);
            }

        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }
}
