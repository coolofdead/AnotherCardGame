using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIManager : MonoBehaviour
{
    public Transform playerCardsHandTransform;
    public CreatureUI creatureUIPrefab;
    public GameManager gameManager;

    public CreatureUI[] playerHandCards => playerCardsHandTransform.GetComponentsInChildren<CreatureUI>();

    private DroppableAreaUI[] handDroppableAreas; 

    private void Awake()
    {
        gameManager.player.hand.onCardAdded += UpdatePlayerHandUI;

        handDroppableAreas = playerCardsHandTransform.GetComponentsInChildren<DroppableAreaUI>();
    }

    private void UpdatePlayerHandUI(Hand hand, CreatureSO creatureSO)
    {
        CreatureUI creatureUI = Instantiate(creatureUIPrefab, playerCardsHandTransform);
        creatureUI.SetCreatureSO(creatureSO);

        handDroppableAreas[hand.CurrentCardsInHand - 1].PlaceCard(creatureUI.dragableUI, true);
    }

    private void OnDestroy()
    {
        gameManager.player.hand.onCardAdded -= UpdatePlayerHandUI;
    }
}
