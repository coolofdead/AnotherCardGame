using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : AbstractManager<EnemyManager>
{
    public GameManager gameManager;
    public TurnManager turnManager;
    public BattlefieldAreaManager battlefieldAreaManager;

    public Transform battlefieldParent;
    public CreatureUI creatureUIPrefab;

    public void Play()
    {
        int currentMana = Player.MAX_MANA;

        DroppableAreaUI[] availableBattlefieldAreas = battlefieldAreaManager.GetAllBattlefieldAreasAvailable(false);
        int nthBattlefieldArea = 0;

        // Bot choose creature logic
        foreach (CreatureSO creatureSO in gameManager.opponent.hand)
        {
            if (creatureSO.stats.manaCost <= currentMana && nthBattlefieldArea < availableBattlefieldAreas.Length)
            {
                currentMana -= creatureSO.stats.manaCost;

                SummonAt(creatureSO, availableBattlefieldAreas[nthBattlefieldArea]);
                nthBattlefieldArea++;
            }
        }
    }

    private void SummonAt(CreatureSO creatureSO, DroppableAreaUI droppableAreaUI)
    {
        CreatureUI creatureUI = Instantiate(creatureUIPrefab);
        creatureUI.SetCreatureSO(creatureSO);

        droppableAreaUI.PlaceCard(creatureUI.dragableUI);
        turnManager.EnemyPlayCreature(droppableAreaUI, creatureUI);
    }
}
