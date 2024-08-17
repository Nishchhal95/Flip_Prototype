using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private TMP_Text turnCountTextField;
    [SerializeField] private GameObject gameOverUIPanel;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private TMP_Text gameOverScoreTextField;

    private void OnEnable()
    {
        GameController.ScoreChanged += OnScoreChanged;
        GameController.TurnCountChanged += OnTurnCountChanged;
        GameController.GameOver += OnGameOver;
        
        restartGameButton.onClick.AddListener(RestartGameButtonClicked);
    }

    private void OnDisable()
    {
        GameController.ScoreChanged -= OnScoreChanged;
        GameController.TurnCountChanged -= OnTurnCountChanged;
        GameController.GameOver -= OnGameOver;
        
        restartGameButton.onClick.RemoveListener(RestartGameButtonClicked);
    }

    private void OnScoreChanged(int score)
    {
        scoreTextField.SetText($"Score : {score}");
    }
    
    private void OnTurnCountChanged(int turnCount)
    {
        turnCountTextField.SetText($"Turns : {turnCount}");
    }

    private void OnGameOver(int score)
    {
        gameOverScoreTextField.SetText($"Score : {score}");
        gameOverUIPanel.SetActive(true);
    }

    private void RestartGameButtonClicked()
    {
        // Loads the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
