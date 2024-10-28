using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class ItemConfiguration
{
    [field: SerializeField]
    public string Name { get; set; }
    [field: SerializeField]
    public float Price { get; set; }
    [field: SerializeField]
    public string Category { get; set; }

    public ItemConfiguration(string name, float price, string category)
    {
        Name = name;
        Price = price;
        Category = category;
    }
}
