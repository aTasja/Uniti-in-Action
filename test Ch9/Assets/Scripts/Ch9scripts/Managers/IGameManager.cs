using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameManager {
    ManagerStatus status { get; }

    // Use this for initialization
    void Startup(NetworkService service); 
	
}
