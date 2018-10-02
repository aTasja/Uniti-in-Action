using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IGameManager {

    public ManagerStatus status { get; private set; }

    public int Health { get; private set; }
    public int MaxHealth { get; private set; }

    private NetworkService _network;

    public void Startup (NetworkService service) {
        Debug.Log("Player manager starting...");

        _network = service;

        //Health = 50;
        //MaxHealth = 100;
        UpdateData(50, 100);

        status = ManagerStatus.Started;
	}

    public void UpdateData(int health, int maxHealth)
    {
        this.Health = health;
        this.MaxHealth = maxHealth;
    }

    public void ChangeHealth(int value)
    {
        Health += value;
        if(Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        else if(Health < 0){
            Health = 0;
        }

        if (Health == 0)
        {
            Messenger.Broadcast(GameEvent.LEVEL_FAILED);
        }
       
        //Debug.Log("Health: " + Health + " / " + MaxHealth);
        Messenger.Broadcast(GameEvent.HEALTH_UPDATED);

    }
    public void Respawn()
    {
        UpdateData(50, 100);
    }

}
