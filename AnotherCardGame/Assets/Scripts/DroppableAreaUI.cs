using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroppableAreaUI : MonoBehaviour
{
    public static Action<AreaType, AreaType, DroppableAreaUI, DragableUI> onElementMovedTo;
    public static Action<AreaType, DroppableAreaUI, DragableUI> onElementPlaced;

    public enum AreaType { None, Battlefield, Hand }
    public AreaType areaType = AreaType.None;

    public bool isEnable;

    public GameObject outlineFeedback;
    public Transform cardHandler;

    private bool isHovering;
    public DragableUI ElemOnArea { get; private set; }

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
            PlaceCard(elem);
        }
    }

    public void PlaceCard(DragableUI elem, bool isAnInstantiate = false)
    {
        elem.transform.SetParent(cardHandler);
        elem.transform.localPosition = Vector3.zero;
        elem.transform.localScale = Vector3.one;

        if (elem != ElemOnArea)
        {
            ElemOnArea = elem;

            if (isAnInstantiate == false)
                onElementPlaced?.Invoke(areaType, this, elem);
        }
    }

    private void FreeArea(AreaType fromAreaType, DroppableAreaUI droppableAreaUI, DragableUI dragableUI)
    {
        if (ElemOnArea == dragableUI && droppableAreaUI != this)
        {
            ElemOnArea = null;
            onElementMovedTo?.Invoke(fromAreaType, areaType, droppableAreaUI, dragableUI);
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
