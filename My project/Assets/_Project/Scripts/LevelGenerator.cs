using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator Instance { get; private set; }
    
    [SerializeField] private int itemWidth = 200;
    [SerializeField] private int itemHeight = 200;
    [SerializeField] private int itemXGap = 100;
    [SerializeField] private int itemYGap = 100;
    [SerializeField] private int numberOfRows = 4;
    [SerializeField] private int numberOfColumns = 5;
    [SerializeField] private bool stylizedHide = false;
    
    [SerializeField] private GameController gameController;
    [SerializeField] private RectTransform content;
    [SerializeField] private GridItem gridItemPrefab;
    [SerializeField] private List<Card> cards;

    public GridItem[,] GridItemsArray { get; private set; }
    public int InGameCardCount { get; private set; }
    private List<Card> inGameCards = new List<Card>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        SetupCards();
        SpawnLevel();
        StartCoroutine(HideGridItemsRoutine());
        SetupGameController();
    }
    private void SetupCards()
    {
        LoadCards();
        
        List<Card> copyCards = new List<Card>(cards);
        Shuffle(copyCards);
        
        InGameCardCount = (numberOfRows * numberOfColumns) / 2;
        inGameCards = new List<Card>();
        for (int i = 0; i < InGameCardCount; i++)
        {
            inGameCards.Add(copyCards[i]);
        }
        inGameCards.AddRange(inGameCards);
        Shuffle(inGameCards);
    }

    private void LoadCards()
    {
        cards = new List<Card>();
        Sprite[] iconSprites = Resources.LoadAll<Sprite>("Icons");
        for (int i = 0; i < iconSprites.Length; i++)
        {
            cards.Add(new Card()
            {
                ID = i,
                Sprite = iconSprites[i]
            });
        }
    }

    private void SpawnLevel()
    {
        GridItemsArray = new GridItem[numberOfRows, numberOfColumns];
        int cardIndex = 0;
        Vector2 startingPoint =
            new Vector2(-content.rect.width / 2 + itemWidth / 2, content.rect.height / 2 - itemHeight / 2);

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                GridItem gridItem = Instantiate(gridItemPrefab, content);
                gridItem.SetCard(inGameCards[cardIndex]);
                
                RectTransform gridItemRectTransform = (RectTransform)gridItem.transform;
                Vector2 gridItemPosition = startingPoint + new Vector2((itemWidth + itemXGap) * j, (-itemHeight - itemYGap) * i);
                gridItemRectTransform.anchoredPosition = gridItemPosition;
                gridItemRectTransform.sizeDelta = new Vector2(itemWidth, itemHeight);
                gridItem.transform.name = i + "," + j;
                gridItem.ArrayIndex = new Vector2Int(i, j);
                
                GridItemsArray[i, j] = gridItem;
                cardIndex++;
            }
        }
    }

    private IEnumerator HideGridItemsRoutine()
    {
        yield return new WaitForSecondsRealtime(2f);
        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                GridItem gridItem = GridItemsArray[i, j];
                gridItem.Hide();
                if (stylizedHide)
                {
                    yield return new WaitForSecondsRealtime(0.01f);
                }
            }
        }
    }
    
    private void SetupGameController()
    {
        gameController.Init();
    }
    
    public GridItem GetItemByIndex(Vector2Int arrayIndex)
    {
        if (!IsIndexInBounds(arrayIndex.x, arrayIndex.y))
        {
            return null;
        }
        return GridItemsArray[arrayIndex.x, arrayIndex.y];
    }
    
    private bool IsIndexInBounds(int x, int y)
    {
        if(x < 0 || y < 0 || x >= numberOfRows || y >= numberOfColumns)
        {
            return false;
        }

        return true;
    }
    
    private void Shuffle<T>(List<T> list)  
    {  
        System.Random rng = new System.Random(); 
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = rng.Next(n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }
}
