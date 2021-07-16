using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Text Subtitle;
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button MenuButton;
    [SerializeField] private GameObject Panel;


    private void Awake()
    {
        PlayButton.onClick.AddListener(PlayGame);
        MenuButton.onClick.AddListener(BackMenu);

        TriggerService.Instance.AddListener(TriggerType.GAME_OVER, Show);
    }

    public void Show(TriggerType triggerType , object win)
    {
        Subtitle.text = (bool)win ? "Won!" : "Defeated!";

        Panel.SetActive(true);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    void BackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
