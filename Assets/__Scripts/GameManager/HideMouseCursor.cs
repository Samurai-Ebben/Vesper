using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideMouseCursor : MonoBehaviour
{
    public bool cursorVisible = false;
    void Start()
    {
        Cursor.visible = cursorVisible;
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            Cursor.visible = cursorVisible;
        }
    }
}
