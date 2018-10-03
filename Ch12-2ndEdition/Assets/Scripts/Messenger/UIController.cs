using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class UIController : MonoBehaviour
{
    [SerializeField] private Text healthLabel;
    [SerializeField] private InventoryPopup popup;
    [SerializeField] private Text levelEnding;
    [SerializeField] private AudioPopup audioPopup;
    void Awake()
    {
        Messenger.AddListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.AddListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.AddListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.AddListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }
    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.HEALTH_UPDATED, OnHealthUpdated);
        Messenger.RemoveListener(GameEvent.LEVEL_COMPLETE, OnLevelComplete);
        Messenger.RemoveListener(GameEvent.LEVEL_FAILED, OnLevelFailed);
        Messenger.RemoveListener(GameEvent.GAME_COMPLETE, OnGameComplete);
    }
    void Start()
    {
        OnHealthUpdated();

        levelEnding.gameObject.SetActive(false);
        popup.gameObject.SetActive(false);
        audioPopup.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            bool isShowing = popup.gameObject.activeSelf;
            popup.gameObject.SetActive(!isShowing);
            popup.Refresh();
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            bool isShowing = audioPopup.gameObject.activeSelf;
            audioPopup.gameObject.SetActive(!isShowing);
            if (isShowing)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }
    private void OnHealthUpdated()
    {
        string message = "Health: " + Managers.Player.Health + "/" + Managers.
        Player.MaxHealth;
        healthLabel.text = message;
    }

    private void OnLevelComplete()
    {
        StartCoroutine(CompleteLevel());
    }

    private IEnumerator CompleteLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Complete!";

        yield return new WaitForSeconds(2);

        Managers.Mission.GoToNext();
    }

    private void OnLevelFailed()
    {
        StartCoroutine(FailLevel());
    }

    private IEnumerator FailLevel()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "Level Failed";

        yield return new WaitForSeconds(2);

        Managers.Player.Respawn();
        Managers.Mission.RestartCurrent();
    }

    public void SaveGame()
    {
        Managers.Data.SaveGameState();
    }
    public void LoadGame()
    {
        Managers.Data.LoadGameState();
    }

    private void OnGameComplete()
    {
        levelEnding.gameObject.SetActive(true);
        levelEnding.text = "You Finished the Game!";
    }
}
