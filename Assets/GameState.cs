using UnityEngine;

public class GameState : MonoBehaviour
{
    public static GameState I;

    public bool noteRead;
    public bool plantMoved;
    public bool keyTaken;

    void Awake()
    {
        if (I == null) I = this;
        else Destroy(gameObject);
    }
}
