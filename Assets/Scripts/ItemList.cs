using System;
using System.Collections.Generic;
using UnityEngine;
public class ItemList : MonoBehaviour
{
    private struct Item
    {
        public string rewardName;
        public double collectivePercentage;
    }

    private List<Item> itemList = new List<Item>();
    private double collectivePercentage;
    private System.Random rand = new System.Random();

    public void AddEntry(string itemName, double percentage)
    {
        collectivePercentage += percentage;
        itemList.Add(new Item { rewardName = itemName, collectivePercentage = collectivePercentage });
    }

    
    public string GetItem()
    {
        double r = rand.NextDouble() * collectivePercentage;
        foreach (Item item in itemList)
        {
            if (item.collectivePercentage >= r)
            {
                return item.rewardName;
            }
        }
        return "Empty";
    }
}
