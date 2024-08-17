using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GridItem : MonoBehaviour, IPointerClickHandler
{
    public Action<GridItem> OnItemClicked;
    [field: SerializeField] public int ID { get; set; }
    [field: SerializeField] public Vector2Int ArrayIndex { get; set; }
    [SerializeField] private Image icon;
    [SerializeField] private bool isShown;

    private Coroutine flipRoutnie;
    
    public void SetCard(Card card)
    {
        ID = card.ID;
        icon.sprite = card.Sprite;
    }

    public void Show()
    {
        if (flipRoutnie != null)
        {
            StopCoroutine(flipRoutnie);
        }
        flipRoutnie = StartCoroutine(FlipRoutine(transform.localScale, 
            new Vector3(0, transform.localScale.y, transform.localScale.z),
                .1f,
            true));
        isShown = true;
    }

    public void Hide()
    {
        if (flipRoutnie != null)
        {
            StopCoroutine(flipRoutnie);
        }
        flipRoutnie = StartCoroutine(FlipRoutine(transform.localScale, 
            new Vector3(0, transform.localScale.y, transform.localScale.z), 
                .1f, 
            false));
        isShown = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (isShown)
        {
            return;
        }
        
        Show();
        OnItemClicked?.Invoke(this);
    }

    private IEnumerator FlipRoutine(Vector3 startScale, Vector3 targetScale, float duration, bool show)
    {
        float timeElapsed = 0;
        float localDuration = duration;
        while (transform.localScale.x > targetScale.x)
        {
            transform.localScale = Vector3.Lerp(startScale, targetScale, timeElapsed / localDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = targetScale;
        icon.gameObject.SetActive(show);

        timeElapsed = 0;
        localDuration = duration;
        while (transform.localScale.x < startScale.x)
        {
            transform.localScale = Vector3.Lerp(targetScale, startScale, timeElapsed / localDuration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        
        transform.localScale = startScale;
    }
}

[Serializable]
public class Card
{
    public int ID;
    public Sprite Sprite;
}
