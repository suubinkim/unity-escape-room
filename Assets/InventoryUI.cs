using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image slotBg1;        // Slot1
    public Image slotIcon1;      // SlotIcon1
    public Sprite keySprite;     // 🔑 열쇠 스프라이트

    Inventory inv;

    void Start()
    {
        inv = Object.FindFirstObjectByType<Inventory>();
        Refresh();
    }

    public void Refresh()
    {
        if (inv == null || slotIcon1 == null) return;

        bool hasKey = inv.Has("Key");

        // 🔑 아이템 있으면 아이콘 표시
        slotIcon1.enabled = hasKey;

        if (hasKey)
        {
            slotIcon1.sprite = keySprite;   // ⭐ 여기 중요
        }
        else
        {
            slotIcon1.sprite = null;
            Selection.Clear();
        }

        // 선택 강조 색
        if (slotBg1 != null)
        {
            slotBg1.color = Selection.Is("Key")
                ? new Color(1f, 1f, 0.6f, 1f)
                : Color.white;
        }
    }

    // 슬롯 클릭 시 선택/해제
    public void OnClickSlot1()
{
    if (inv == null) return;

    if (!inv.Has("Key"))
    {
        Selection.Clear();
        return;
    }

    // 이미 선택돼 있으면 해제
    if (Selection.Is("Key"))
    {
        Selection.Clear();
    }
    else
    {
        // 🔑 선택
        Selection.Select("Key");

        // ⭐ 아이템 설명 팝업
        if (PopupUI.I != null)
            PopupUI.I.Show("It's an old key.");
    }

    Refresh();
}

}
