using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TurnManager : AbstractManager<TurnManager>
{
    public const float DELAY_TO_REVEAL = 2f;
    public const int TOTAL_ROUND = 3;

    public GameManager gameManager;
    public BattlefieldAreaManager battlefieldAreaManager;

    public Transform roundsParent;

    private Dictionary<DroppableAreaUI, CreatureUI> creatureUISummonedThisTurnByArea = new Dictionary<DroppableAreaUI, CreatureUI>();

    public int Round { get; private set; }

    private SummonGameEvent summonGameEvent;

    protected override void Awake()
    {
        base.Awake();
        gameManager.onGameStateChanged += OnGameStateChangedListenCardsMove;
    }

    private void OnGameStateChangedListenCardsMove(GameState currentGameState)
    {
        if (currentGameState == GameState.PlaceCreatures)
        {
            DroppableAreaUI.onElementMovedTo += OnCreaturePlacedOnBattlefield;
        }
        else
        {
            DroppableAreaUI.onElementMovedTo -= OnCreaturePlacedOnBattlefield;
        }
    }

    private void OnCreaturePlacedOnBattlefield(DropableAreaType fromArea, DropableAreaType toArea, DroppableAreaUI fromDroppableAreaUI, DroppableAreaUI toDroppableAreaUI, DragableUI dragableUI)
    {
        if (fromArea == DropableAreaType.Battlefield && toArea == DropableAreaType.Hand)
        {
            creatureUISummonedThisTurnByArea.Remove(fromDroppableAreaUI);
        }

        if (fromArea == DropableAreaType.Hand && toArea == DropableAreaType.Battlefield)
        {
            creatureUISummonedThisTurnByArea.Add(toDroppableAreaUI, dragableUI.GetComponent<CreatureUI>());
        }
    }

    public IEnumerator RevealCreaturesSummonedThisTurn()
    {
        print("start revealing creatures");

        summonGameEvent = new SummonGameEvent();
        summonGameEvent.totalPlayerSummon = 0;
        summonGameEvent.nthPlayerSummon = 0;

        foreach (KeyValuePair<DroppableAreaUI, CreatureUI> creatureUIByBattlefieldArea in creatureUISummonedThisTurnByArea)
        {
            creatureUIByBattlefieldArea.Value.HideAndLock();
        }

        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            CreatureUI creatureUIToSummon;
            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(true, i), out creatureUIToSummon))
            {
                summonGameEvent.faceOffCreature = battlefieldAreaManager.GetCreatureOnField(false, i);
                yield return SummonCreatureAtBattlefieldArea(creatureUIToSummon, battlefieldAreaManager.GetBattlefieldArea(true, i));
                summonGameEvent.totalPlayerSummon++;
            }

            if (creatureUISummonedThisTurnByArea.TryGetValue(battlefieldAreaManager.GetBattlefieldArea(false, i), out creatureUIToSummon))
            {
                summonGameEvent.faceOffCreature = battlefieldAreaManager.GetCreatureOnField(true, i);
                yield return SummonCreatureAtBattlefieldArea(creatureUIToSummon, battlefieldAreaManager.GetBattlefieldArea(true, i));
            }

            if (creatureUIToSummon != null)
                yield return new WaitForSeconds(DELAY_TO_REVEAL);
        }

        yield return GameEventManager.TriggerEvent(new AllCreatureSummonedGameEvent() { totalPlayerSummon = summonGameEvent.totalPlayerSummon });

        creatureUISummonedThisTurnByArea.Clear();

        print("all creatures has been revealed");

        yield return new WaitForSeconds(2f);
    }

    private IEnumerator SummonCreatureAtBattlefieldArea(CreatureUI creatureUI, DroppableAreaUI droppableAreaUI)
    {
        creatureUI.Summon(DELAY_TO_REVEAL);

        summonGameEvent.summonedCreature = creatureUI;
        summonGameEvent.nthPlayerSummon++;
        summonGameEvent.isPlayerCreature = droppableAreaUI.isControlledByPlayer;

        yield return GameEventManager.TriggerEvent(summonGameEvent);
    }

    public void EnemyPlayCreature(DroppableAreaUI droppableAreaUI, CreatureUI creatureUI)
    {
        creatureUISummonedThisTurnByArea.Add(droppableAreaUI, creatureUI);
    }

    public IEnumerator MoveToNextRound()
    {
        Round++;

        roundsParent.GetChild(0).GetChild(1).gameObject.SetActive(true);

        foreach (CreatureUI creatureUIOnBattlefield in battlefieldAreaManager.GetAllCreaturesOnBattlefield())
        {
            creatureUIOnBattlefield.ResetTempStats();
        }

        yield return GameEventManager.TriggerEvent(new EndTurnGameEvent());
    }

    private void OnDestroy()
    {
        gameManager.onGameStateChanged -= OnGameStateChangedListenCardsMove;
    }
}
