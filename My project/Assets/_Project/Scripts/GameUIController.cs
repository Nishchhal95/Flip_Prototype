using TMPro;
using UnityEngine;

public class GameUIController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreTextField;
    [SerializeField] private TMP_Text turnCountTextField;

    private void OnEnable()
    {
        GameController.ScoreChanged += OnScoreChanged;
        GameController.TurnCountChanged += OnTurnCountChanged;
    }

    private void OnDisable()
    {
        GameController.ScoreChanged -= OnScoreChanged;
        GameController.TurnCountChanged -= OnTurnCountChanged;
    }

    private void OnScoreChanged(int score)
    {
        scoreTextField.SetText($"Score : {score}");
    }
    
    private void OnTurnCountChanged(int turnCount)
    {
        turnCountTextField.SetText($"Turns : {turnCount}");
    }
}
