using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Image slotBg1;      // Slot1(Image) 드래그
    public Image slotIcon1;    // SlotIcon1(Image) 드래그

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
        if (!inv.Has("Key")) Selection.Clear();

        slotIcon1.enabled = hasKey;

    }

    // Slot1 버튼에 연결할 함수
    public void OnClickSlot1()
    {
        if (inv == null) return;

        if (!inv.Has("Key"))
        {
            Selection.Clear();
        }
        else
        {
            // 토글 선택
            if (Selection.Is("Key")) Selection.Clear();
            else Selection.Select("Key");
        }

        Refresh();
    }
}
