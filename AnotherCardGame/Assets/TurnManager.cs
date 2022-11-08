using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public const float DELAY_TO_REVEAL = 2f;

    public BattlefieldAreaManager battlefieldAreaManager;

    private Dictionary<DroppableAreaUI, CreatureUI> creatureUISummonedThisTurnByArea = new Dictionary<DroppableAreaUI, CreatureUI>();

    private void Awake()
    {
        DroppableAreaUI.onElementMovedTo += OnCreaturePlacedOnBattlefield;
    }

    private void OnCreaturePlacedOnBattlefield(DroppableAreaUI.AreaType fromArea, DroppableAreaUI.AreaType toArea, DroppableAreaUI droppableAreaUI, DragableUI dragableUI)
    {
        if (fromArea == DroppableAreaUI.AreaType.Battlefield && toArea == DroppableAreaUI.AreaType.Hand)
        {
            creatureUISummonedThisTurnByArea.Remove(droppableAreaUI);
        }

        if (fromArea == DroppableAreaUI.AreaType.Hand && toArea == DroppableAreaUI.AreaType.Battlefield)
        {
            creatureUISummonedThisTurnByArea.Add(droppableAreaUI, dragableUI.GetComponent<CreatureUI>());
        }
    }

    public IEnumerator RevealCreaturesSummonedThisTurn()
    {
        print("start revealing creatures");

        foreach (KeyValuePair<DroppableAreaUI, CreatureUI> creatureUIByBattlefieldArea in creatureUISummonedThisTurnByArea)
        {
            creatureUIByBattlefieldArea.Value.Hide();
        }

        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            CreatureUI creatureUIToSummon;
            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(true,  i), out creatureUIToSummon))
            {
                creatureUIToSummon.Summon(DELAY_TO_REVEAL);
            }

            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(false, i), out creatureUIToSummon))
            {
                creatureUIToSummon.Summon(DELAY_TO_REVEAL);
            }

            if (creatureUIToSummon != null)
                yield return new WaitForSeconds(DELAY_TO_REVEAL);
        }

        creatureUISummonedThisTurnByArea.Clear();

        print("all creatures has been revealed");

        yield return new WaitForSeconds(3f);
    }

    public void EnemyPlayCreature(DroppableAreaUI droppableAreaUI, CreatureUI creatureUI)
    {
        creatureUISummonedThisTurnByArea.Add(droppableAreaUI, creatureUI);
    }

    private void OnDestroy()
    {
        DroppableAreaUI.onElementMovedTo -= OnCreaturePlacedOnBattlefield;
    }
}
