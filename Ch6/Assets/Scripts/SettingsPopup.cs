using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPopup : MonoBehaviour {


    [SerializeField] private Slider speedSlider;

    void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1);
    }

    public void Open()

    {
        gameObject.SetActive(true); //активирует объект, чтобы открыть окно
    }

    public void Close()
    {
        gameObject.SetActive(false); //деактевирует, чтобы закрыть
    }

    public void OnSubmitName(string name)
    {
        Debug.Log("name");
    }

    public void OnSpeedValue(float speed)
    {
        //Debug.Log("Speed: " + speed);
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }
}

