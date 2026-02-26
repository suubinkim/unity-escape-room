using UnityEngine;
using UnityEngine.InputSystem;

public abstract class ClickableWorld2D : MonoBehaviour
{
    Camera cam;
    float ignoreUntil;

    protected virtual void Awake()
    {
        cam = Camera.main;
        ignoreUntil = Time.time + 0.2f;
    }

void Update()
{
    var mouse = UnityEngine.InputSystem.Mouse.current;
    if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return;
    
    Vector2 screen = mouse.position.ReadValue();
    Vector3 w = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
    Vector2 worldPos = new Vector2(w.x, w.y);
    Collider2D hit = Physics2D.OverlapPoint(worldPos);

    if (hit == null || hit.gameObject != gameObject) return;
    OnClicked();
}

    protected abstract void OnClicked();

    bool WasClick(out Vector2 worldPos)
{
    worldPos = default;
    var mouse = UnityEngine.InputSystem.Mouse.current;
    if (mouse == null || !mouse.leftButton.wasPressedThisFrame) return false;
    Vector2 screen = mouse.position.ReadValue();
    Vector3 w = cam.ScreenToWorldPoint(new Vector3(screen.x, screen.y, 0f));
    worldPos = new Vector2(w.x, w.y);
    return true;
}
}
