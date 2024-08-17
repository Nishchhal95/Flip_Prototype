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
    [SerializeField] private List<Sprite> icons;

    public GridItem[,] GridItemsArray { get; private set; }
    private List<Sprite> subIcons = new List<Sprite>();

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
        SetupIcons();
        SpawnLevel();
        StartCoroutine(HideGridItemsRoutine());
        SetupGameController();
    }
    private void SetupIcons()
    {
        Shuffle(icons);
        int subIconArrayCount = numberOfRows * numberOfColumns;
        for (int i = 0; i < subIconArrayCount / 2; i++)
        {
            subIcons.Add(icons[i]);
        }

        subIcons.AddRange(subIcons);
        Shuffle(subIcons);
    }

    private void SpawnLevel()
    {
        GridItemsArray = new GridItem[numberOfRows, numberOfColumns];
        int iconIndex = 0;
        Vector2 startingPoint =
            new Vector2(-content.rect.width / 2 + itemWidth / 2, content.rect.height / 2 - itemHeight / 2);

        for (int i = 0; i < numberOfRows; i++)
        {
            for (int j = 0; j < numberOfColumns; j++)
            {
                GridItem gridItem = Instantiate(gridItemPrefab, content);
                gridItem.SetIcon(subIcons[iconIndex]);
                gridItem.iconID = subIcons[iconIndex].name;
                
                RectTransform gridItemRectTransform = (RectTransform)gridItem.transform;
                Vector2 gridItemPosition = startingPoint + new Vector2((itemWidth + itemXGap) * j, (-itemHeight - itemYGap) * i);
                gridItemRectTransform.anchoredPosition = gridItemPosition;
                gridItemRectTransform.sizeDelta = new Vector2(itemWidth, itemHeight);
                gridItem.transform.name = i + "," + j;
                gridItem.ArrayIndex = new Vector2Int(i, j);
                
                GridItemsArray[i, j] = gridItem;
                iconIndex++;
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
    
    private void Shuffle(List<Sprite> list)  
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
