using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HandAreaManager : AbstractManager<HandAreaManager>
{
    public Transform playerCardsHandTransform;
    public GameManager gameManager;

    private DroppableAreaUI[] handDroppableAreas;

    protected override void Awake()
    {
        base.Awake();
        handDroppableAreas = playerCardsHandTransform.GetComponentsInChildren<DroppableAreaUI>();
    }

    public DroppableAreaUI GetFirstEmptyHandArea()
    {
        return handDroppableAreas.FirstOrDefault(handDroppableArea => handDroppableArea.IsAvailable);
    }
}
