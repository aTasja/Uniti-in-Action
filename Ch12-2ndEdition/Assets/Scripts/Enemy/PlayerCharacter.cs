using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour {

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip impactSound;

    public void Hurt(int damage)
    {
        audioSource.PlayOneShot(impactSound);
        Managers.Player.ChangeHealth(-damage);
    }
}
