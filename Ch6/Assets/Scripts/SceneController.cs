using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject enemyPrefab;
    private GameObject _enemy;
    private float speed;


    void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    void OnSpeedChanged(float value)
    {
        speed = value;
    }



    // Update is called once per frame
    void Update () {
		if(_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.GetComponent<WanderingAI>().speed = speed;
            _enemy.transform.Rotate(0, angle, 0);
            
        }
	}
}
