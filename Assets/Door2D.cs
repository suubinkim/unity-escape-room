using System.Collections;
using UnityEngine;

public class Door2D : MonoBehaviour
{
    [Header("Requirement")]
    public string requiredItem = "Key";

    [Header("Sprites (optional)")]
    public Sprite closedSprite;
    public Sprite openSprite;

    [Header("Messages")]
    public string lockedMessage = "The door is locked.";
    public string openedMessage = "The door is open!";

    [Header("Behavior")]
    public bool disableColliderWhenOpened = true;

    Camera cam;
    SpriteRenderer sr;
    Collider2D col;
    bool opened;
    float ignoreUntil;

    void Awake()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        col = GetComponent<Collider2D>();
        ignoreUntil = Time.time + 0.2f;

        if (sr != null && closedSprite != null)
            sr.sprite = closedSprite;
    }

    void Update()
    {
        if (opened) return;
        if (Time.time < ignoreUntil) return;
        if (cam == null) return;

        if (!WasClick(out Vector2 worldPos)) return;

        Collider2D hit = Physics2D.OverlapPoint(worldPos);
        if (hit == null || hit.gameObject != gameObject) return;

        TryOpen();
    }

    void TryOpen()
{
    Inventory inv = FindInventory();
    if (inv == null)
    {
        Debug.LogError("Inventory not found in scene!");
        return;
    }
    // 가짜 열쇠로 시도했을 때
    if (Selection.Is("FakeKey"))
    {
        if (PopupUI.I != null)
            PopupUI.I.Show("The key doesn't fit...");
        StartCoroutine(Shake());
        return;
    }

    // 선택된 아이템이 Key가 아니면 잠김 처리
    if (!inv.Has(requiredItem) || !Selection.Is(requiredItem))
    {
        if (PopupUI.I != null)
            PopupUI.I.Show(lockedMessage);

        StartCoroutine(Shake());
        return;
    }

    // ✅ 소비: 인벤토리에서 제거 + 선택 해제
    inv.Remove(requiredItem);
    Selection.Clear();

    opened = true;
    
    if (sr != null && openSprite != null)
    sr.sprite = openSprite;

    if (disableColliderWhenOpened && col != null)
    col.enabled = false;

    if (sr != null && openSprite != null)
        sr.sprite = openSprite;

    if (PopupUI.I != null)
        PopupUI.I.Show(openedMessage);

    if (disableColliderWhenOpened && col != null)
        col.enabled = false;

    Object.FindFirstObjectByType<InventoryUI>()?.Refresh();
}



    Inventory FindInventory()
    {
        return Object.FindFirstObjectByType<Inventory>();
    }

    IEnumerator Shake()
    {
        Vector3 basePos = transform.position;
        float t = 0f;

        while (t < 0.25f)
        {
            t += Time.deltaTime;
            float x = Mathf.Sin(t * 80f) * 0.05f;
            transform.position = basePos + new Vector3(x, 0f, 0f);
            yield return null;
        }

        transform.position = basePos;
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
