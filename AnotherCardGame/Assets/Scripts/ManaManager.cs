using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;
    public HandUIManager handUIManager;

    private void Awake()
    {
        DroppableAreaUI.onElementMovedTo += OnCreatureMovedUpdateMana;
        gameManager.player.onManaChanged += CheckManaCostRequired;
    }

    private void OnCreatureMovedUpdateMana(DroppableAreaUI.AreaType fromArea, DroppableAreaUI.AreaType toArea, DroppableAreaUI fromDroppableAreaUI, DroppableAreaUI toDroppableAreaUI, DragableUI dragableUI)
    {
        CreatureUI creatureUI = dragableUI.GetComponent<CreatureUI>();

        if (fromArea == DroppableAreaUI.AreaType.Hand && toArea == DroppableAreaUI.AreaType.Battlefield)
        {
            gameManager.player.PayManaCost(creatureUI.creatureSO.stats.manaCost);
        }
        
        if (fromArea == DroppableAreaUI.AreaType.Battlefield && toArea == DroppableAreaUI.AreaType.Hand)
        {
            gameManager.player.RefundManaCost(creatureUI.creatureSO.stats.manaCost);
        }
    }

    private void CheckManaCostRequired(int mana)
    {
        foreach (CreatureUI creatureUI in handUIManager.playerHandCards)
        {
            creatureUI.CanBeSummoned(creatureUI.stats.manaCost <= mana);
        }
    }

    private void OnDestroy()
    {
        DroppableAreaUI.onElementMovedTo -= OnCreatureMovedUpdateMana;
        gameManager.player.onManaChanged -= CheckManaCostRequired;
    }
}
