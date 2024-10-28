using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    public ContextMenu contextMenu;

    public TMP_Text total;

    public SummaryPanel summaryPanel;


    public void Setup()
    {
        summaryPanel.Setup();
    }

    public void ShowContextMenuAtPosition(Vector2 position, ItemObject hittedObject)
    {
        contextMenu.SetToPosition(position, hittedObject);
    }

    public void HideContextMenu()
    {
        contextMenu.Hide();
    }

    public void UpdateItemsData(List<ItemObject> items)
    {
        var sumPrice = items.Sum(c => c.configuration.Price);
        total.text = sumPrice.ToString() + "$";

        summaryPanel.UpdateSummary(items);
    }

    

}
