using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCursor : MonoBehaviour
{
    public Texture2D mouseCursor;

    Vector2 hotSpot = new Vector2(0, 0);
    CursorMode cursorMode = CursorMode.ForceSoftware;

    private void Start()
    {
        Cursor.SetCursor(mouseCursor, hotSpot, cursorMode);
    }
}
