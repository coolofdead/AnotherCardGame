using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : AbstractManager<ManaManager>
{
    [Header("Managers")]
    public GameManager gameManager;
    public HandUIManager handUIManager;

    protected override void Awake()
    {
        base.Awake();
        gameManager.player.onManaChanged += CheckManaCostRequired;
        gameManager.onGameStateChanged += OnGameStateChangedListenCardsMove;
    }

    private void OnGameStateChangedListenCardsMove(GameState currentGameState)
    {
        if (currentGameState == GameState.PlaceCreatures)
        {
            DroppableAreaUI.onElementMovedTo += OnCreatureMovedUpdateMana;
        }
        else
        {
            DroppableAreaUI.onElementMovedTo -= OnCreatureMovedUpdateMana;
        }
    }

    private void OnCreatureMovedUpdateMana(DropableAreaType fromArea, DropableAreaType toArea, DroppableAreaUI fromDroppableAreaUI, DroppableAreaUI toDroppableAreaUI, DragableUI dragableUI)
    {
        if (fromDroppableAreaUI.isControlledByPlayer == false)
            return;

        CreatureUI creatureUI = dragableUI.GetComponent<CreatureUI>();

        if (fromArea == DropableAreaType.Hand && toArea == DropableAreaType.Battlefield)
        {
            gameManager.player.PayManaCost(creatureUI.BaseStats.manaCost);
        }
        
        if (fromArea == DropableAreaType.Battlefield && toArea == DropableAreaType.Hand)
        {
            gameManager.player.RefundManaCost(creatureUI.BaseStats.manaCost);
        }
    }

    private void CheckManaCostRequired(int mana)
    {
        foreach (CreatureUI creatureUI in handUIManager.playerHandCards)
        {
            creatureUI.CanBeSummon(creatureUI.Stats.manaCost <= mana);
        }
    }

    private void OnDestroy()
    {
        gameManager.player.onManaChanged -= CheckManaCostRequired;
        gameManager.onGameStateChanged -= OnGameStateChangedListenCardsMove;
    }
}
