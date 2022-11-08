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

    public IEnumerator Fight(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        playerBattlingUI.SetCreature(playerCreatureUI);
        opponentBattlingUI.SetCreature(opponentCreatureUI);

        battlePanel.SetActive(true);
        
        yield return new WaitWhile(() => battlePanelTimeline.state == PlayState.Playing);

        // Attack and block with shield...
        if (playerCreatureUI.stats.power >= (opponentCreatureUI.stats.power + opponentCreatureUI.stats.shield)) // Player tie or more
        {
            playerBattlingUI.KillOpponent(opponentBattlingUI);
        }

        if (opponentCreatureUI.stats.power >= (playerCreatureUI.stats.power + playerCreatureUI.stats.shield)) // Opponent tie or more
        {
            opponentBattlingUI.KillOpponent(playerBattlingUI);
        }

        if (playerCreatureUI.stats.power + playerCreatureUI.stats.shield > opponentCreatureUI.stats.power && playerCreatureUI.stats.shield > 0)
        {
            playerBattlingUI.SurvivesWithShield();
        }

        if (opponentCreatureUI.stats.power + opponentCreatureUI.stats.shield > playerCreatureUI.stats.power && opponentCreatureUI.stats.shield > 0)
        {
            opponentBattlingUI.SurvivesWithShield();
        }

        yield return new WaitUntil(() => playerBattlingUI.DoneDoingAnimations && opponentBattlingUI.DoneDoingAnimations);

        yield return new WaitForSeconds(delayDisplayEndFight);

        battlePanel.SetActive(false);
    }
}
