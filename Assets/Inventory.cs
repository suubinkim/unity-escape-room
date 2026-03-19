using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();

    public void Add(ItemData item)
    {
        if (Has(item.id)) return; // 중복 방지
        items.Add(item);
        Debug.Log("Inventory Add: " + item.id);
    }

    public bool Has(string id) => items.Exists(i => i.id == id);

    public ItemData Get(string id) => items.Find(i => i.id == id);

    public bool Remove(string id)
    {
        var item = Get(id);
        if (item == null) return false;
        items.Remove(item);
        Debug.Log("Inventory Remove: " + id);
        return true;
    }

    public List<ItemData> GetAll() => items;
}