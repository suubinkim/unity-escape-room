using UnityEngine;

public class PlantInteractable : ClickableWorld2D
{
    public Transform movedTo;      // 치웠을 때 위치(빈 오브젝트로 표시)
    public GameObject keyObject;   // 열쇠 오브젝트 (처음엔 비활성)

    public string movedMsg = "You moved the plant.";
    public string hintMsg = "Maybe something is under it...";

    protected override void OnClicked()
    {
        Debug.Log("OnClicked 호출됨!");
        Debug.Log($"GameState={GameState.I}, plantMoved={GameState.I?.plantMoved}, movedTo={movedTo}");
        if (GameState.I == null) return;

        // 이미 키까지 먹었으면 그냥 메시지 없이 무시하거나, 다른 메시지
        if (GameState.I.keyTaken)
        {
            if (PopupUI.I != null) PopupUI.I.Show("Nothing else here.");
            return;
        }

        // 이미 치운 상태면 힌트만
        if (GameState.I.plantMoved)
        {
            if (PopupUI.I != null) PopupUI.I.Show("The plant is already moved.");
            return;
        }

        // ✅ 메모를 안 봐도 치울 수 있음
        GameState.I.plantMoved = true;

        if (movedTo != null)
            transform.position = movedTo.position;

        // 열쇠 드러나게
        if (keyObject != null)
            keyObject.SetActive(true);

        // 메모를 봤으면 더 자연스러운 문구, 안 봤어도 치운 건 치움
        if (PopupUI.I != null)
        {
            PopupUI.I.Show(GameState.I.noteRead ? "The key was under the plant!" : movedMsg, 1.6f);
        }
    }
}
