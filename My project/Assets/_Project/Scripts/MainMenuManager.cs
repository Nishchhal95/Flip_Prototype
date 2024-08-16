using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(PlayButtonClicked);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(PlayButtonClicked);
    }

    private void PlayButtonClicked()
    {
        // Basically loads the next scene in the list of scenes.
        // Make sure to setup the order of scenes right in the Build Settings :)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
