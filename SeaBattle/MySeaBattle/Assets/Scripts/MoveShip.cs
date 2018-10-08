using UnityEngine;

public class MoveShip : MonoBehaviour {

    [SerializeField]
    private float moveSpeed = 1;

    public int torpedas = 3;

    private void Update()    {

        if (transform.position.x < -11f)
            Destroy(gameObject);

        transform.position -= new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

    
}
