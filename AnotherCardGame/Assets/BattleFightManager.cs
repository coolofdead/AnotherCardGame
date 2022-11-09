using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattleFightManager : MonoBehaviour
{
    [Header("Managers")]
    public GameManager gameManager;
    public BattleManager battleManager;
    public BattlefieldAreaManager battlefieldAreaManager;

    public IEnumerator ProcessFights()
    {
        int totalPlayerDamageReceived = 0;
        int totalOpponentDamageReceived = 0;
        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            CreatureUI playerCreatureUI = battlefieldAreaManager.GetCreatureOnField(true, i);
            CreatureUI opponentCreatureUI = battlefieldAreaManager.GetCreatureOnField(false, i);

            if (playerCreatureUI == null && opponentCreatureUI == null) continue;

            yield return battleManager.AnnounceFight(battlefieldAreaManager.GetBattlefieldArea(true, i), battlefieldAreaManager.GetBattlefieldArea(false, i));

            yield return battleManager.Fight(playerCreatureUI, opponentCreatureUI);

            totalPlayerDamageReceived += battleManager.PlayerDamageForLastBattle;
            totalOpponentDamageReceived += battleManager.OpponentDamageForLastBattle;
        }

        gameManager.player.ReceiveDamage(totalPlayerDamageReceived);
        gameManager.opponent.ReceiveDamage(totalOpponentDamageReceived);

        // Make dead creature dies on battlefield
        foreach (CreatureUI creatureUIDeadThisTurn in battleManager.deadCreatures)
        {
            creatureUIDeadThisTurn.ShowDeath();
        }

        yield return new WaitUntil(() => battleManager.deadCreatures.All((deadCreature) => deadCreature == null));

        battleManager.deadCreatures.Clear();
    }
}
