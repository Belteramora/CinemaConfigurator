using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemButton: MonoBehaviour
{
    private Button _button;
    private Action<ItemConfiguration, GameObject> _onChooseItem;

    public TMP_Text descriptionText;

    public string itemName;
    public float price;
    public string category;
    public GameObject prefab;

    public void Setup(Action<ItemConfiguration, GameObject> onChooseItem)
    {
        _onChooseItem = onChooseItem;

        _button = GetComponentInChildren<Button>();
        _button.onClick.AddListener(OnChooseItem);

        descriptionText.text = itemName + "\n" + price.ToString() + "$";
    }

    public void OnChooseItem()
    {
        _onChooseItem?.Invoke(new ItemConfiguration(itemName, price, category), prefab);
    }
}


