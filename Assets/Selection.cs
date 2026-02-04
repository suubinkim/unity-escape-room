using UnityEngine;

public static class Selection
{
    public static string selectedItemId = null;

    public static void Select(string id)
    {
        selectedItemId = id;
        Debug.Log("Selected: " + id);
    }

    public static void Clear()
    {
        selectedItemId = null;
        Debug.Log("Selected cleared");
    }

    public static bool Is(string id) => selectedItemId == id;
}
