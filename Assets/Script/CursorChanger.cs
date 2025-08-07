using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorChanger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Texture2D DefaultCursor;
    public Texture2D ClickCursor;
    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(ClickCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);
    }
}
