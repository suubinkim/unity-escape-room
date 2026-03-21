using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slotPrefab;    // 슬롯 프리팹
    public Transform slotParent;     // 슬롯들 담을 부모 오브젝트
    public int slotCount = 4;  // ← Inspector에서 개수 지정

    Inventory inv;
    List<InventorySlotUI> slots = new List<InventorySlotUI>();

    void Start()
    {
        inv = Object.FindFirstObjectByType<Inventory>();
        InitSlots();  // ← 시작할 때 빈 슬롯 미리 생성
    }
    void InitSlots()
    {
        for (int i = 0; i < slotCount; i++)
        {
            var slot = Instantiate(slotPrefab, slotParent);
            var slotUI = slot.GetComponent<InventorySlotUI>();
            slotUI.Init();  // ← 빈 상태로 초기화
            slots.Add(slotUI);
        }
    }

    public void Refresh()
    {
        if (inv == null) return;
        var items = inv.GetAll();
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < items.Count)
                slots[i].Setup(items[i]);  // ← 아이템 있으면 채우기
            else
                slots[i].Init();           // ← 없으면 빈 슬롯
        }
    }
}