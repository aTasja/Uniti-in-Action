using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneController : MonoBehaviour {

    [SerializeField] private GameObject shipPrefab;
    private GameObject _ship;



    // Update is called once per frame
    void Update() {
		if(_ship == null)
        {
            
            int posOfShip = Random.value > 0.5 ? 37 : -37;
            _ship = Instantiate(shipPrefab) as GameObject;
            _ship.transform.position = new Vector3(posOfShip, 1f, 36);

        }
	}
}
