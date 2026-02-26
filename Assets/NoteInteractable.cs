using UnityEngine;

public class NoteInteractable : ClickableWorld2D
{
    [TextArea] public string noteText =
        "Note:\nThere is a key under the plant.";

    protected override void OnClicked()
    {
        if (GameState.I == null) return;

        GameState.I.noteRead = true;

        if (PopupUI.I != null)
            PopupUI.I.Show(noteText, 2.0f);
    }
}
