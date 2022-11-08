using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class DragableUI : MonoBehaviour
{
    public static Action<DragableUI> onDragStarted;
    public static Action<DragableUI> onDragStopped;
    public static Action<DragableUI> onDrag;

    public bool isEnabled = true;

    private Canvas canvas;
    private Vector3 defaultPos;

    private void Awake()
    {
        canvas = FindObjectOfType<Canvas>();
    }

    public void BeginDragHandler(BaseEventData baseEventData)
    {
        if (isEnabled == false) return;

        defaultPos = transform.position;

        onDragStarted?.Invoke(this);
    }

    public void DragHandler(BaseEventData baseEventData)
    {
        if (isEnabled == false) return;

        PointerEventData pointerEventData = (PointerEventData)baseEventData;

        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerEventData.position, canvas.worldCamera, out position);

        transform.position = canvas.transform.TransformPoint(position);
        onDrag?.Invoke(this);
    }

    public void EndHandler(BaseEventData baseEventData)
    {
        if (isEnabled == false) return;

        transform.position = defaultPos;
        onDragStopped?.Invoke(this);
    }
}
