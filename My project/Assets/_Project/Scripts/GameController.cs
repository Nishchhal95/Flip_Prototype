using System;
using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action<int> ScoreChanged;
    public static Action<int> TurnCountChanged;

    public static Action CardFlipped;
    public static Action CorrectMatch;
    public static Action IncorrectMatch;
    public static Action GameOver;
    
    [SerializeField] private GridItem firstItem;
    [SerializeField] private GridItem secondItem;
    [SerializeField] private int score;
    [SerializeField] private int turnCount;
    
    public void Init()
    {
        for (int i = 0; i < LevelGenerator.Instance.GridItemsArray.GetLength(0); i++)
        {
            for (int j = 0; j < LevelGenerator.Instance.GridItemsArray.GetLength(1); j++)
            {
                GridItem gridItem = LevelGenerator.Instance.GridItemsArray[i, j];
                gridItem.OnItemClicked += GridItemClicked;
            }
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < LevelGenerator.Instance.GridItemsArray.GetLength(0); i++)
        {
            for (int j = 0; j < LevelGenerator.Instance.GridItemsArray.GetLength(1); j++)
            {
                GridItem gridItem = LevelGenerator.Instance.GridItemsArray[i, j];
                gridItem.OnItemClicked -= GridItemClicked;
            }
        }
    }

    private void GridItemClicked(GridItem gridItem)
    {
        StartCoroutine(GridItemClickedRoutine(gridItem));
    }

    private IEnumerator GridItemClickedRoutine(GridItem gridItem)
    {
        CardFlipped?.Invoke();
        if (firstItem == null)
        {
            firstItem = gridItem;
            yield break;
        }

        secondItem = gridItem;

        if (firstItem.iconID == secondItem.iconID)
        {
            // Keep it open and add Score
            score += 10;
            ScoreChanged?.Invoke(score);
            CorrectMatch?.Invoke();
        }
        else
        {
            yield return new WaitForSecondsRealtime(.5f);
            firstItem.Hide();
            secondItem.Hide();
            IncorrectMatch?.Invoke();
        }

        firstItem = null;
        secondItem = null;
        turnCount++;
        TurnCountChanged?.Invoke(turnCount);
    }
}
