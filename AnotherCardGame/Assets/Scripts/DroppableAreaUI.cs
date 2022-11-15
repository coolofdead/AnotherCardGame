using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroppableAreaUI : MonoBehaviour
{
    public static Action<DropableAreaType, DropableAreaType, DroppableAreaUI, DroppableAreaUI, DragableUI> onElementMovedTo;
    public static Action<DropableAreaType, DroppableAreaUI, DragableUI> onElementPlaced;

    public DropableAreaType areaType = DropableAreaType.None;

    public bool isControlledByPlayer = true;
    public bool isEnable;
    public bool IsAvailable => ElemOnArea == null;

    public GameObject outlineFeedback;
    public Transform cardHandler;

    private bool isHovering;
    public DragableUI ElemOnArea { get; private set; }

    public GameObject battleAnnounce;

    private void Awake()
    {
        DragableUI.onDragStarted += SetCurrentObjectDragging;
        DragableUI.onDrag += CheckDragableIsHoveringArea;
        DragableUI.onDragStopped += PlaceCardIfHovering;
        
        onElementPlaced += FreeArea;
    }

    private void SetCurrentObjectDragging(DragableUI dragableUI)
    {
        isHovering = false;
    }

    private void CheckDragableIsHoveringArea(DragableUI dragableUI)
    {
        if (!isEnable || ElemOnArea != null || dragableUI == ElemOnArea)
            return;

        isHovering = RectTransformUtility.RectangleContainsScreenPoint((RectTransform)transform, dragableUI.transform.position);
        outlineFeedback.SetActive(isHovering);
    }

    private void PlaceCardIfHovering(DragableUI elem)
    {
        outlineFeedback.SetActive(false);

        if (isHovering && ElemOnArea == null)
        {
            MoveCard(elem);
        }
    }

    public void MoveCard(DragableUI elem)
    {
        PlaceCard(elem);

        onElementPlaced?.Invoke(areaType, this, elem);
    }

    public void PlaceCard(DragableUI elem) // Place physicaly a card but don't fire any events
    {
        elem.transform.SetParent(cardHandler);
        elem.transform.localPosition = Vector3.zero;
        elem.transform.localScale = Vector3.one;

        ElemOnArea = elem;
    }

    private void FreeArea(DropableAreaType toAreaType, DroppableAreaUI droppableAreaUI, DragableUI dragableUI)
    {
        if (ElemOnArea == dragableUI && droppableAreaUI != this)
        {
            ElemOnArea = null;
            onElementMovedTo?.Invoke(areaType, toAreaType, this, droppableAreaUI, dragableUI);
        }
    }

    private void OnDestroy()
    {
        DragableUI.onDragStarted -= SetCurrentObjectDragging;
        DragableUI.onDrag -= CheckDragableIsHoveringArea;
        DragableUI.onDragStopped -= PlaceCardIfHovering;

        onElementPlaced -= FreeArea;
    }
}
