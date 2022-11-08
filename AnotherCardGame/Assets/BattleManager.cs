using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject battlePanel;

    public CreatureBattlingUI playerBattlingUI;
    public CreatureBattlingUI opponentBattlingUI;

    public IEnumerator Fight(CreatureUI playerCreatureUI, CreatureUI opponentCreatureUI)
    {
        playerBattlingUI.SetCreature(playerCreatureUI);
        opponentBattlingUI.SetCreature(opponentCreatureUI);

        battlePanel.SetActive(true);

        // Attack and block with shield...

        yield return new WaitForSeconds(20);

        battlePanel.SetActive(false);
    }
}
