using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedrawEffect : AbstractCreatureEffect
{
    private bool choicesValidated = false;

    public override IEnumerator Activate(GameEvent gameEvent)
    {
        SetupRedraw(gameEvent);

        yield return new WaitWhile(() => !choicesValidated);
    }

    private void SetupRedraw(GameEvent gameEvent)
    {
        choicesValidated = false;

        AllCreatureSummonedGameEvent allCreatureSummonedGameEvent = gameEvent as AllCreatureSummonedGameEvent;

        ChoicesDetails choiceDetails = new ChoicesDetails()
        {
            choiceType = ChoicesType.DroppableArea,
            nbChoices = allCreatureSummonedGameEvent.totalPlayerSummon,
            callbackOnSelection = OnChoicesValidate
        };
        ChoiceManager.Instance.SetupChoices(choiceDetails);
    }

    private void OnChoicesValidate(List<DragableUI> choicesDragablesUI)
    {
        choicesValidated = true;

        int nbCardsToRedraw = choicesDragablesUI.Count;
        foreach (DragableUI dragableUI in choicesDragablesUI)
        {
            CreatureUI creatureUI = dragableUI.GetComponent<CreatureUI>();

            GameManager.Instance.player.deck.PutBackToDeck(creatureUI.creatureSO);
            UnityEngine.Object.Destroy(creatureUI.gameObject);
        }

        for (int i = 0; i < nbCardsToRedraw; i++)
            GameManager.Instance.player.Draw();
    }

    public override EffectTiming GetActivationTimings()
    {
        return EffectTiming.OnAllCreatureSummoned;
    }

    public override bool RequirementsMet(GameEvent gameEvent)
    {
        return true;
    }

    public override Effect GetEffectType()
    {
        return Effect.Redraw;
    }

    public override string GetEffectDescription()
    {
        return "Redraw X where X equal total summon you made this turn";
    }
}
