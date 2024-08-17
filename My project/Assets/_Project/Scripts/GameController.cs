using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static Action<int> ScoreChanged;
    public static Action<int> TurnCountChanged;

    public static Action CardFlipped;
    public static Action CorrectMatch;
    public static Action IncorrectMatch;
    public static Action<int> GameOver;
    
    // [SerializeField] private GridItem firstItem;
    // [SerializeField] private GridItem secondItem;
    [SerializeField] private int score;
    [SerializeField] private int matchCount;
    [SerializeField] private int turnCount;

    private Stack<GridItem> clikedItemStack = new();
    private bool IsGameOver = false;
    
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
        CardFlipped?.Invoke();
        clikedItemStack.Push(gridItem);

        if (clikedItemStack.Count >= 2)
        {
            GridItem firstItem = clikedItemStack.Pop();
            GridItem secondItem = clikedItemStack.Pop();

            StartCoroutine(CheckMatchRoutine(firstItem, secondItem));
        }
    }

    // private void GridItemClicked(GridItem gridItem)
    // {
    //     if (IsGameOver)
    //     {
    //         return;
    //     }
    //     
    //     CardFlipped?.Invoke();
    //     if (firstItem == null)
    //     {
    //         firstItem = gridItem;
    //         return;
    //     }
    //     
    //     secondItem = gridItem;
    //     StartCoroutine(CheckMatchRoutine());
    // }

    private IEnumerator CheckMatchRoutine(GridItem firstItem, GridItem secondItem)
    {
        yield return new WaitForSecondsRealtime(.2f);
        if (firstItem.ID == secondItem.ID)
        {
            score += 10;
            matchCount++;
            ScoreChanged?.Invoke(score);
            CorrectMatch?.Invoke();
            firstItem.gameObject.SetActive(false);
            secondItem.gameObject.SetActive(false);

            if (matchCount >= LevelGenerator.Instance.InGameCardCount)
            {
                GameOver?.Invoke(score);
                IsGameOver = true;
            }
        }
        else
        {
            yield return new WaitForSecondsRealtime(.3f);
            firstItem.Hide();
            secondItem.Hide();
            IncorrectMatch?.Invoke();
        }
        turnCount++;
        TurnCountChanged?.Invoke(turnCount);
    }
}
