using System.Collections;
using TMPro;
using UnityEngine;

public class PopupUI : MonoBehaviour
{
    public static PopupUI I;
    public TextMeshProUGUI popupText;
    Coroutine co;

    void Awake()
    {
        if (I == null) I = this;
        else Destroy(gameObject);

        if (popupText != null) popupText.gameObject.SetActive(false);
    }

    public void Show(string msg, float seconds = 1.2f)
    {
        if (popupText == null) return;

        if (co != null) StopCoroutine(co);
        co = StartCoroutine(ShowCo(msg, seconds));
    }

    IEnumerator ShowCo(string msg, float seconds)
    {
        popupText.text = msg;
        popupText.gameObject.SetActive(true);
        yield return new WaitForSeconds(seconds);
        popupText.gameObject.SetActive(false);
        co = null;
    }
}
