
using UnityEngine;


public class MoveAim : MonoBehaviour {

    public Transform aim;
    [SerializeField]
    private float speed = 10f;
    

    private void OnMouseDrag()    {
        if (!PressFire.lose)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.x = mousePos.x > 7.25f ? 7.25f : mousePos.x;
            mousePos.x = mousePos.x < -7.25f ? -7.25f : mousePos.x;
            aim.position = Vector2.MoveTowards(aim.position,
                new Vector2(mousePos.x, aim.position.y),
                speed * Time.deltaTime);
        }
    }

    public Vector2 getAimPos()
    {
        return aim.position;
    }

   
}
