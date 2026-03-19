using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/ItemData")]
public class ItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public string description;
    public Sprite icon;
}