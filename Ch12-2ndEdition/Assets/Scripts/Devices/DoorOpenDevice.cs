using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenDevice : BaseDevice {
    [SerializeField] private Vector3 dPos; //смещение, применяемое при открывании двери


    private bool _open; // переименная для слежением за открытием двери

    private Vector3 initialPos;
    private Vector3 openedPos;

    // Use this for initialization
    void Start()
    {
        _open = false;
        initialPos = gameObject.transform.position;
        openedPos = initialPos - dPos;
    }

    override
    public void Operate()
    {
        if (_open) // открываем и закрываем дверь в зависимости от ее состояния
        {
            iTween.MoveTo(gameObject, iTween.Hash("y", initialPos.y, "time", 3f));
        }
        else if (!_open)
        {
            iTween.MoveTo(gameObject, iTween.Hash("y", openedPos.y, "time", 3f));
        }
        
        _open = !_open;
    }
    
	public void Activate()
    {
        if (!_open)
        {
            iTween.MoveTo(GameObject.Find(gameObject.name), iTween.Hash("y", openedPos.y, "time", 3f));
            _open = true;
        }
    }

    public void Deactivate()
    {
        if (_open)
        {
            iTween.MoveTo(GameObject.Find(gameObject.name), iTween.Hash("y", initialPos.y, "time", 3f));
            _open = false;
        }
            
    }
}
