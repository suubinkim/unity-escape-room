using UnityEngine;

public class PlantInteractable : ClickableWorld2D
{
    public Transform movedTo;      // 치웠을 때 위치(빈 오브젝트로 표시)
    public GameObject keyObject;   // 열쇠 오브젝트 (처음엔 비활성)

    public string movedMsg = "You moved the plant.";
    public string hintMsg = "Maybe something is under it...";
    Vector3 _originalPos;

    void Start()
    {
        _originalPos = transform.position;
    }

    protected override void OnClicked()
    {
        if (GameState.I == null) return;


        // 이미 치운 상태면 제자리로 돌아오기
        if (GameState.I.plantMoved)
        {
            GameState.I.plantMoved = false;
            transform.position = _originalPos;  // 원래 위치로;
            if (PopupUI.I != null) PopupUI.I.Show("You moved the plant back.");
            return;
        }

        // 치우기
        _originalPos = transform.position;  // 원래 위치 저장
        GameState.I.plantMoved = true;

        if (movedTo != null)
            transform.position = movedTo.position;

        if (keyObject != null && !GameState.I.keyTaken)
            keyObject.SetActive(true);

        if (PopupUI.I != null)
            PopupUI.I.Show(GameState.I.noteRead ? "The key was under the plant!" : movedMsg, 1.6f);
    }
}
