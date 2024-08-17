using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button loadButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
        loadButton.onClick.AddListener(LoadButtonClicked);

        loadButton.interactable = PlayerPrefs.HasKey(PersistanceManager.GAME_STATE_KEY);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayButtonClicked);
        loadButton.onClick.RemoveListener(LoadButtonClicked);
    }

    private void PlayButtonClicked()
    {
        // Basically loads the next scene in the list of scenes.
        // Make sure to setup the order of scenes right in the Build Settings :)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void LoadButtonClicked()
    {
        PersistanceManager.loadGame = true;
        PlayButtonClicked();
    }
}
