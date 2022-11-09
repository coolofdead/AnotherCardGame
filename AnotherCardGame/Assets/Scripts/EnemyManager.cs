using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public TurnManager turnManager;
    public BattlefieldAreaManager battlefieldAreaManager;

    public Transform battlefieldParent;
    public CreatureUI creatureUIPrefab;

    public void Play()
    {
        int currentMana = Player.MAX_MANA;

        List<DroppableAreaUI> availableBattlefieldAreas = new List<DroppableAreaUI>();
        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            DroppableAreaUI droppableAreaUI = battlefieldAreaManager.GetBattlefieldArea(false, i);
            if (droppableAreaUI.IsAvailable)
            {
                availableBattlefieldAreas.Add(droppableAreaUI);
            }
        }

        // Bot choose creature logic
        foreach (CreatureSO creatureSO in gameManager.opponent.hand)
        {
            if (creatureSO.stats.manaCost <= currentMana && availableBattlefieldAreas.Count > 0)
            {
                currentMana -= creatureSO.stats.manaCost;

                SummonAt(creatureSO, availableBattlefieldAreas[0]);

                availableBattlefieldAreas.RemoveAt(0);
            }
        }
    }

    private void SummonAt(CreatureSO creatureSO, DroppableAreaUI droppableAreaUI)
    {
        CreatureUI creatureUI = Instantiate(creatureUIPrefab);
        creatureUI.SetCreatureSO(creatureSO);

        droppableAreaUI.PlaceCard(creatureUI.dragableUI, true);
        turnManager.EnemyPlayCreature(droppableAreaUI, creatureUI);
    }
}
