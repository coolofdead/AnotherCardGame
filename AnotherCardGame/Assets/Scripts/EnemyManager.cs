using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        List<DroppableAreaUI> availableBattlefieldAreas = battlefieldAreaManager.GetAllBattlefieldAreasAvailable(false);
        availableBattlefieldAreas = availableBattlefieldAreas.Randomize().ToList();

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

        droppableAreaUI.PlaceCard(creatureUI.dragableUI);
        turnManager.EnemyPlayCreature(droppableAreaUI, creatureUI);
    }
}
