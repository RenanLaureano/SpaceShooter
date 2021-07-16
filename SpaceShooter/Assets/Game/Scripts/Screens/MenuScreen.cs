using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MenuScreen : MonoBehaviour
{

    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;
 
    void Start()
    {
        PlayButton.onClick.AddListener(PlayGame);
        QuitButton.onClick.AddListener(QuitGame);
    }

    void PlayGame()
    {
        SceneManager.LoadScene("GamePlay");
    }

    void QuitGame()
    {
        Application.Quit();
    }
}
