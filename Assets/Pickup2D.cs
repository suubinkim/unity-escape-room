using UnityEngine;

public class Pickup2D : MonoBehaviour
{
    public ItemData itemData;           // ← string itemId 대신 ItemData로 교체
    public string pickupMessage = "Got it!";

    Camera cam;
    float ignoreUntil;
    Inventory inv;
InventoryUI invUI;

void Awake()
{
    cam = Camera.main;
    ignoreUntil = Time.time + 0.2f;
    inv = Object.FindFirstObjectByType<Inventory>();      // ← Awake로 이동
    invUI = Object.FindFirstObjectByType<InventoryUI>(); // ← Awake로 이동
}

void Update()
{
    if (Time.time < ignoreUntil) return;
    if (cam == null) return;
    if (!WasClick(out Vector2 worldPos)) return;
    Collider2D hit = Physics2D.OverlapPoint(worldPos);
    if (hit == null || hit.gameObject != gameObject) return;

    if (inv == null) { Debug.LogError("Inventory not found!"); return; }
    if (itemData == null) { Debug.LogError("ItemData not assigned!"); return; }

    inv.Add(itemData);
    invUI?.Refresh();
    if (PopupUI.I != null) PopupUI.I.Show(pickupMessage);
    gameObject.SetActive(false);
}

    bool WasClick(out Vector2 worldPos)
    {
        worldPos = default;
#if ENABLE_INPUT_SYSTEM
        var mouse = UnityEngine.InputSystem.Mouse.current;
        if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return false;
        Vector2 screen = mouse.position.ReadValue();
        Vector3 w = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
        worldPos = new Vector2(w.x, w.y);
        return true;
#else
        if (!Input.GetMouseButtonDown(0)) return false;
        Vector3 w = cam.ScreenToWorldPoint(Input.mousePosition);
        worldPos = new Vector2(w.x, w.y);
        return true;
#endif
    }
}