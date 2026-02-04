using UnityEngine;

public class Pickup2D : MonoBehaviour
{
    public string itemId = "Key";
    public string pickupMessage = "Got the key!";

    Camera cam;
    float ignoreUntil;

    void Awake()
    {
        cam = Camera.main;
        ignoreUntil = Time.time + 0.2f; // 시작 직후 Play 클릭 입력 무시
    }

    void Update()
    {
        if (Time.time < ignoreUntil) return;
        if (cam == null) return;

        if (!WasClick(out Vector2 worldPos)) return;

        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit == null || hit.gameObject != gameObject) return;

        Inventory inv = FindInventory();
        if (inv == null)
        {
            Debug.LogError("Inventory not found in scene! GameManager에 Inventory 붙였는지 확인!");
            return;
        }

        inv.Add(itemId);
        Object.FindFirstObjectByType<InventoryUI>()?.Refresh();


        if (PopupUI.I != null)
            PopupUI.I.Show(pickupMessage);

        gameObject.SetActive(false);
    }

    Inventory FindInventory()
    {
        // Unity 2022+ / Unity 6에서 사용 가능
        return Object.FindFirstObjectByType<Inventory>();
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
