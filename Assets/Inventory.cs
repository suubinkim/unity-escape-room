using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private HashSet<string> items = new HashSet<string>();

    public void Add(string id)
    {
        items.Add(id);
        Debug.Log("Inventory Add: " + id);
    }

    public bool Has(string id)
    {
        bool has = items.Contains(id);
        Debug.Log("Inventory Has(" + id + "): " + has);
        return has;
    }

    public bool Remove(string id)
    {
        bool removed = items.Remove(id);
        Debug.Log("Inventory Remove(" + id + "): " + removed);
        return removed;
    }
}
