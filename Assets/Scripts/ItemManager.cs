using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static List<string> categories;

    public List<ItemObject> items = new List<ItemObject>();

    public Transform objectsRoot;

    public ContextMenu contextMenu;

    public UI ui;
    public RoomController controller;

    public List<ItemButton> buttonsToSetup;

    public void Start()
    {
        categories = controller.categoryLimit.Keys.ToList();

        foreach (var itemButton in buttonsToSetup)
        {
            itemButton.Setup(AddItem);
        }

        ui.Setup();
        contextMenu.Setup(DeleteItem);
    }

    public void AddItem(ItemConfiguration itemConfig, GameObject prefab)
    {
        if (controller.room == null) return;

        if (controller.alreadyBought[itemConfig.Category] >= controller.categoryLimit[itemConfig.Category]) return;

        var instance = Instantiate(prefab, objectsRoot);

        var itemObject = instance.GetComponent<ItemObject>();
        itemObject.Setup(itemConfig);

        items.Add(itemObject);

        controller.AddItem(itemConfig.Category, instance);

        ui.UpdateItemsData(items);
    }

    public void DeleteItem(ItemObject itemObject)
    {
        controller.DeleteItem(itemObject.configuration.Category);

        items.Remove(itemObject);

        Destroy(itemObject.gameObject, 0.01f);



        ui.UpdateItemsData(items);

    }
}
