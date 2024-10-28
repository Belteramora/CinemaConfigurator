using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ItemObject: MonoBehaviour
{
    public ItemConfiguration configuration;
    public void Setup(ItemConfiguration config)
    {
        this.configuration = config;
    }
}