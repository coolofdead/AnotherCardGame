using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIManager : AbstractManager<HandUIManager>
{
    public Transform playerCardsHandTransform;
    public CreatureUI creatureUIPrefab;
    public GameManager gameManager;
    public HandAreaManager handAreaManager;

    public CreatureUI[] playerHandCards => playerCardsHandTransform.GetComponentsInChildren<CreatureUI>();

    protected override void Awake()
    {
        base.Awake();
        gameManager.player.deck.onCardDrawn += UpdatePlayerHandUI;
        DroppableAreaUI.onElementMovedTo += OnCardPlacedUpdateHand;
    }

    private void OnCardPlacedUpdateHand(DropableAreaType fromArea, DropableAreaType toArea, DroppableAreaUI fromDroppableAreaUI, DroppableAreaUI toDroppableAreaUI, DragableUI dragableUI)
    {
        if (fromArea == DropableAreaType.Hand)
        {
            gameManager.player.hand.RemoveCard(dragableUI.GetComponent<CreatureUI>().creatureSO);
        }

        if (toArea == DropableAreaType.Hand)
        {
            gameManager.player.hand.AddCard(dragableUI.GetComponent<CreatureUI>().creatureSO);
        }
    }

    private void UpdatePlayerHandUI(Hand hand, CreatureSO creatureSO)
    {
        CreatureUI creatureUI = Instantiate(creatureUIPrefab, playerCardsHandTransform);
        creatureUI.SetCreatureSO(creatureSO);

        handAreaManager.GetFirstEmptyHandArea().PlaceCard(creatureUI.dragableUI);
    }

    private void OnDestroy()
    {
        gameManager.player.deck.onCardDrawn -= UpdatePlayerHandUI;
        DroppableAreaUI.onElementMovedTo -= OnCardPlacedUpdateHand;
    }
}
