using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleManager : AbstractManager<BattleManager>
{
    public GameObject battlePanel;
    public PlayableDirector battlePanelTimeline;

    public CreatureBattlingUI playerBattlingUI;
    public CreatureBattlingUI opponentBattlingUI;

    public float delayDisplayEndFight = 1f;
    public float delayBeforeNextFight = 3f;
    public float timeDisplayAnnounceFight = 2f;
    
    public int PlayerDamageForLastBattle { get; protected set; }
    public int OpponentDamageForLastBattle { get; protected set; }

    public List<CreatureUI> deadCreatures = new List<CreatureUI>();

    public IEnumerator AnnounceFight(DroppableAreaUI playerCreatureBattlefieldArea, DroppableAreaUI opponentCreatureBattlefieldArea)
    {
        playerCreatureBattlefieldArea.battleAnnounce.SetActive(playerCreatureBattlefieldArea.ElemOnArea != null);
        opponentCreatureBattlefieldArea.battleAnnounce.SetActive(opponentCreatureBattlefieldArea.ElemOnArea != null);

        yield return new WaitForSeconds(timeDisplayAnnounceFight);

        playerCreatureBattlefieldArea.battleAnnounce.SetActive(false);
        opponentCreatureBattlefieldArea.battleAnnounce.SetActive(false);
    }

    public IEnumerator Fight(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        PlayerDamageForLastBattle = 0;
        OpponentDamageForLastBattle = 0;

        playerBattlingUI.SetCreature(playerCreatureUI);
        opponentBattlingUI.SetCreature(opponentCreatureUI);

        battlePanel.SetActive(true);
        
        yield return new WaitWhile(() => battlePanelTimeline.state == PlayState.Playing);

        if (playerCreatureUI == null && opponentCreatureUI.canAttackDirectly || opponentCreatureUI == null && playerCreatureUI.canAttackDirectly)
        {
            DirectAttack(playerCreatureUI, opponentCreatureUI);
        }
        else
        {
            Battle(playerCreatureUI, opponentCreatureUI);
        }

        yield return new WaitUntil(() => playerBattlingUI.DoneDoingAnimations && opponentBattlingUI.DoneDoingAnimations);

        yield return new WaitForSeconds(delayDisplayEndFight);

        battlePanel.SetActive(false);

        yield return new WaitForSeconds(delayBeforeNextFight);
    }

    private void Battle(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        if (playerCreatureUI.Stats.power >= (opponentCreatureUI.Stats.power + opponentCreatureUI.Stats.shield)) // Player tie or more
        {
            OpponentDamageForLastBattle = playerCreatureUI.Stats.power - opponentCreatureUI.Stats.power;
            playerBattlingUI.KillOpponent(opponentBattlingUI);
            deadCreatures.Add(opponentCreatureUI);
            opponentCreatureUI.slayerCreatureUI = playerCreatureUI;
        }

        if (opponentCreatureUI.Stats.power >= (playerCreatureUI.Stats.power + playerCreatureUI.Stats.shield)) // Opponent tie or more
        {
            PlayerDamageForLastBattle = opponentCreatureUI.Stats.power - playerCreatureUI.Stats.power;
            opponentBattlingUI.KillOpponent(playerBattlingUI);
            deadCreatures.Add(playerCreatureUI);
            playerCreatureUI.slayerCreatureUI = opponentCreatureUI;
        }

        if (playerCreatureUI.Stats.power + playerCreatureUI.Stats.shield > opponentCreatureUI.Stats.power && playerCreatureUI.Stats.shield > 0)
        {
            playerBattlingUI.SurvivesWithShield();
        }

        if (opponentCreatureUI.Stats.power + opponentCreatureUI.Stats.shield > playerCreatureUI.Stats.power && opponentCreatureUI.Stats.shield > 0)
        {
            opponentBattlingUI.SurvivesWithShield();
        }
    }

    private void DirectAttack(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        if (playerCreatureUI == null)
        {
            PlayerDamageForLastBattle = opponentCreatureUI.Stats.power;
            opponentBattlingUI.KillOpponent(playerBattlingUI);
        }

        if (opponentCreatureUI == null)
        {
            OpponentDamageForLastBattle = playerCreatureUI.Stats.power;
            playerBattlingUI.KillOpponent(opponentBattlingUI);
        }
    }
}
