using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameManager gameManager;
    public TurnManager turnManager;

    public Transform battlefieldParent;
    public CreatureUI creatureUIPrefab;

    public void Play()
    {
        print("enemy is playing");

        int currentMana = Player.MAX_MANA;

        DroppableAreaUI[] droppableAreaUIs = battlefieldParent.GetComponentsInChildren<DroppableAreaUI>();

        // Bot choose creature logic
        List<CreatureSO> creaturesToSummon = new List<CreatureSO>();
        foreach (CreatureSO creatureSO in gameManager.opponent.hand)
        {
            if (creatureSO.stats.manaCost <= currentMana && creaturesToSummon.Count < 3)
            {
                creaturesToSummon.Add(creatureSO);
                currentMana -= creatureSO.stats.manaCost;
            }
        }

        // Bot summons logic
        for (int i = 0; i < creaturesToSummon.Count; i++)
        {
            CreatureUI creatureUI = Instantiate(creatureUIPrefab);
            creatureUI.SetCreatureSO(creaturesToSummon[i]);

            droppableAreaUIs[i].PlaceCard(creatureUI.dragableUI, true);
            turnManager.EnemyPlayCreature(droppableAreaUIs[i], creatureUI);
        }
    }
}
