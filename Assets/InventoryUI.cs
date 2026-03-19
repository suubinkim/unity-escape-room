using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;    // 슬롯 프리팹
    public Transform slotParent;     // 슬롯들 담을 부모 오브젝트

    Inventory inv;
    List<GameObject> slots = new List<GameObject>();

    void Start()
    {
        inv = Object.FindFirstObjectByType<Inventory>();
        Refresh();
    }

public void Refresh()
{
    if (inv == null) return;
    if (slotPrefab == null) return;  // ← 이게 있어야 해요
    if (slotParent == null) return;  // ← 이것도

    foreach (var s in slots) Destroy(s);
    slots.Clear();

    foreach (var item in inv.GetAll())
    {
        var slot = Instantiate(slotPrefab, slotParent);
        var slotUI = slot.GetComponent<InventorySlotUI>();
         if (slotUI == null)  // ← 추가
    {
        Debug.LogError("InventorySlotUI 컴포넌트가 프리팹에 없어요!");
        continue;
    }
        slotUI.Setup(item);
        slots.Add(slot);
    }
}
}