using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextMenu : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private RectTransform rectTransform;

    private ItemObject appendedItem;

    private Action<ItemObject> onDeleteItem;

    public float animationTime;

    public void Setup(Action<ItemObject> onDeleteItem)
    {
        canvasGroup = GetComponent<CanvasGroup>();
        rectTransform = GetComponent<RectTransform>();
        this.onDeleteItem = onDeleteItem;
    }

    public void SetToPosition(Vector2 position, ItemObject item)
    {
        canvasGroup.Show(animationTime);

        if(position.x <= rectTransform.rect.width / 2f)
        {
            position.x = rectTransform.rect.width + 5;
        }
        else if(position.x >= Screen.width - rectTransform.rect.width / 2f)
        {
            position.x = Screen.width - rectTransform.rect.width - 5;
        }

        if (position.y <= rectTransform.rect.height / 2f)
        {
            position.y = rectTransform.rect.height + 5;
        }
        else if (position.y >= Screen.height - rectTransform.rect.height / 2f)
        {
            position.y = Screen.height - rectTransform.rect.height - 5;
        }


        rectTransform.anchoredPosition = position;
        appendedItem = item;
    }

    public void Hide(bool force = false)
    {
        if (IsMouseAtContextMenu() && !force) return;


        canvasGroup.Hide(animationTime);
        appendedItem = null;

    }

    public void DeleteItem()
    {
        if (appendedItem == null) return;

        onDeleteItem?.Invoke(appendedItem);

        Hide(true);
    }

    public bool IsMouseAtContextMenu()
    {
        return Input.mousePosition.x >= rectTransform.position.x - rectTransform.rect.width / 2f ||
            Input.mousePosition.x <= rectTransform.position.x + rectTransform.rect.width / 2f ||
            Input.mousePosition.y >= rectTransform.position.y - rectTransform.rect.height / 2f ||
            Input.mousePosition.y <= rectTransform.position.y + rectTransform.rect.height / 2f;
    }
}
