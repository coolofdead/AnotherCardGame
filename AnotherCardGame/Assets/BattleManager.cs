using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BattleManager : MonoBehaviour
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

        if (playerCreatureUI == null || opponentCreatureUI == null)
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
        if (playerCreatureUI.stats.power >= (opponentCreatureUI.stats.power + opponentCreatureUI.stats.shield)) // Player tie or more
        {
            OpponentDamageForLastBattle = playerCreatureUI.stats.power - opponentCreatureUI.stats.power;
            playerBattlingUI.KillOpponent(opponentBattlingUI);
            deadCreatures.Add(opponentCreatureUI);
            opponentCreatureUI.slayerCreatureUI = playerCreatureUI;
        }

        if (opponentCreatureUI.stats.power >= (playerCreatureUI.stats.power + playerCreatureUI.stats.shield)) // Opponent tie or more
        {
            PlayerDamageForLastBattle = opponentCreatureUI.stats.power - playerCreatureUI.stats.power;
            opponentBattlingUI.KillOpponent(playerBattlingUI);
            deadCreatures.Add(playerCreatureUI);
            playerCreatureUI.slayerCreatureUI = opponentCreatureUI;
        }

        if (playerCreatureUI.stats.power + playerCreatureUI.stats.shield > opponentCreatureUI.stats.power && playerCreatureUI.stats.shield > 0)
        {
            playerBattlingUI.SurvivesWithShield();
        }

        if (opponentCreatureUI.stats.power + opponentCreatureUI.stats.shield > playerCreatureUI.stats.power && opponentCreatureUI.stats.shield > 0)
        {
            opponentBattlingUI.SurvivesWithShield();
        }
    }

    private void DirectAttack(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        if (playerCreatureUI == null)
        {
            PlayerDamageForLastBattle = opponentCreatureUI.stats.power;
            opponentBattlingUI.KillOpponent(playerBattlingUI);
        }

        if (opponentCreatureUI == null)
        {
            OpponentDamageForLastBattle = playerCreatureUI.stats.power;
            playerBattlingUI.KillOpponent(opponentBattlingUI);
        }
    }
}
