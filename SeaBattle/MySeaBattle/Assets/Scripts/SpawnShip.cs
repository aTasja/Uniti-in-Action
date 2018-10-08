using System.Collections;
using UnityEngine;

public class SpawnShip : MonoBehaviour {

    public GameObject ship;

	void Start () {
        StartCoroutine(Spawn());
	}

    IEnumerator Spawn()    {
        while (!PressFire.lose)
        {
            Instantiate(ship, new Vector2(11.0f, 0.87f), Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(15f, 25f));
        
        }
    }
	
	
}
