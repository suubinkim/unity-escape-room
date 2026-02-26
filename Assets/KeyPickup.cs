using UnityEngine;

public class KeyPickup : ClickableWorld2D
{
    public string itemId = "Key";
    public string pickupMsg = "Picked up a key!";

    protected override void OnClicked()
    {
        if (GameState.I == null) return;

        // 화분 안 치웠으면 안 보이게 해두는 게 보통이지만,
        // 혹시 활성화된 상태로 클릭되면 방어
        if (!GameState.I.plantMoved)
        {
            if (PopupUI.I != null) PopupUI.I.Show("I can't reach it.");
            return;
        }

        Inventory inv = Object.FindFirstObjectByType<Inventory>();
        if (inv == null)
        {
            Debug.LogError("Inventory not found!");
            return;
        }

        inv.Add(itemId);
        GameState.I.keyTaken = true;

        if (PopupUI.I != null) PopupUI.I.Show(pickupMsg);

        Object.FindFirstObjectByType<InventoryUI>()?.Refresh();

        gameObject.SetActive(false);
    }
}
