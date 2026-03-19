using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUI : MonoBehaviour
{
    public Image bgImage;
    public Image iconImage;
    ItemData item;

    public void Setup(ItemData data)
    {
        item = data;
        iconImage.sprite = data.icon;
        iconImage.enabled = true;
        Refresh();
    }

    public void Refresh()
    {
        if (bgImage != null)
            bgImage.color = Selection.Is(item.id)
                ? new Color(1f, 1f, 0.6f, 1f)
                : Color.white;
    }

    public void OnClick()
    {
        if (item == null) return;
        if (Selection.Is(item.id))
            Selection.Clear();
        else
        {
            Selection.Select(item.id);
            if (PopupUI.I != null)
                PopupUI.I.Show(item.description);
        }
        Refresh();
    }
}