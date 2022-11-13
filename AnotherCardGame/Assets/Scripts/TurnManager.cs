using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : MonoBehaviour
{
    public const float DELAY_TO_REVEAL = 2f;
    public const int TOTAL_ROUND = 3;

    public BattlefieldAreaManager battlefieldAreaManager;

    public Transform roundsParent;

    private Dictionary<DroppableAreaUI, CreatureUI> creatureUISummonedThisTurnByArea = new Dictionary<DroppableAreaUI, CreatureUI>();

    public int Round { get; private set; }

    private void Awake()
    {
        DroppableAreaUI.onElementMovedTo += OnCreaturePlacedOnBattlefield;
    }

    private void OnCreaturePlacedOnBattlefield(DroppableAreaUI.AreaType fromArea, DroppableAreaUI.AreaType toArea, DroppableAreaUI fromDroppableAreaUI, DroppableAreaUI toDroppableAreaUI, DragableUI dragableUI)
    {
        if (fromArea == DroppableAreaUI.AreaType.Battlefield && toArea == DroppableAreaUI.AreaType.Hand)
        {
            creatureUISummonedThisTurnByArea.Remove(fromDroppableAreaUI);
        }

        if (fromArea == DroppableAreaUI.AreaType.Hand && toArea == DroppableAreaUI.AreaType.Battlefield)
        {
            creatureUISummonedThisTurnByArea.Add(toDroppableAreaUI, dragableUI.GetComponent<CreatureUI>());
        }
    }

    public IEnumerator RevealCreaturesSummonedThisTurn()
    {
        print("start revealing creatures");

        int totalPlayerSummon = 0;

        foreach (KeyValuePair<DroppableAreaUI, CreatureUI> creatureUIByBattlefieldArea in creatureUISummonedThisTurnByArea)
        {
            creatureUIByBattlefieldArea.Value.Hide();
        }

        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            CreatureUI creatureUIToSummon;
            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(true, i), out creatureUIToSummon))
            {
                creatureUIToSummon.Summon(DELAY_TO_REVEAL);
                totalPlayerSummon++;
            }

            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(false, i), out creatureUIToSummon))
            {
                creatureUIToSummon.Summon(DELAY_TO_REVEAL);
            }

            if (creatureUIToSummon != null)
                yield return new WaitForSeconds(DELAY_TO_REVEAL);
        }

        int nthPlayerSummon = 0;
        for (int i = 0; i < creatureUISummonedThisTurnByArea.Count; i++)
        {
            KeyValuePair<DroppableAreaUI, CreatureUI> creatureUIBydroppableAreaUI = creatureUISummonedThisTurnByArea.ElementAt(i);
            DroppableAreaUI droppableAreaUI = creatureUIBydroppableAreaUI.Key;
            CreatureUI creatureUI = creatureUIBydroppableAreaUI.Value;

            if (droppableAreaUI.isControlledByPlayer) nthPlayerSummon++;

            GameEventManager.TriggerEvent(
                new SummonGameEvent()
                    {
                        summonedCreature = creatureUI,
                        nthPlayerSummon = nthPlayerSummon,
                        totalPlayerSummon = totalPlayerSummon,
                        isPlayerCreature = droppableAreaUI.isControlledByPlayer
                    }
            );
        }

        creatureUISummonedThisTurnByArea.Clear();

        print("all creatures has been revealed");

        yield return new WaitForSeconds(3f);
    }

    public void EnemyPlayCreature(DroppableAreaUI droppableAreaUI, CreatureUI creatureUI)
    {
        creatureUISummonedThisTurnByArea.Add(droppableAreaUI, creatureUI);
    }

    public void MoveToNextRound()
    {
        Round++;

        roundsParent.GetChild(0).GetChild(1).gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        DroppableAreaUI.onElementMovedTo -= OnCreaturePlacedOnBattlefield;
    }
}
