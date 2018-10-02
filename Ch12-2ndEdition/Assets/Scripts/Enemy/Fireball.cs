using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour {

    public float speed = 10.0f;
    public int damage = 1;

    //[SerializeField] private AudioSource soundSource;
    //[SerializeField] private AudioClip hitWallSound;
    //[SerializeField] private AudioClip hitPlayerSound;

    // Update is called once per frame
    void Update () {
        transform.Translate(0, 0, speed * Time.deltaTime);
	}

    void OnTriggerEnter(Collider other)
    {
        PlayerCharacter player = other.GetComponent<PlayerCharacter>();
        if (player != null)
        {
            Debug.Log("Player hit");
            player.Hurt(damage);
            //soundSource.PlayOneShot(hitPlayerSound);
        }
        else
        {
            //soundSource.PlayOneShot(hitWallSound);
        }
        Destroy(this.gameObject);
    }
}
