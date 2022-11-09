using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Players")]
    public Player player;
    public Player opponent;

    [Header("UI")]
    public GameObject waitingPanel;
    public GameObject startFightButton;
    public TextMeshProUGUI creatureInHandDescriptionTMP;

    [Header("Managers")]
    public EnemyManager enemyManager;
    public BattleManager battleManager;
    public TurnManager turnManager;
    public BattlefieldAreaManager battlefieldAreaManager;
    public BattleFightManager battleFightManager;

    private void Start()
    {
        // DEBUG
        player.FillHandAndResetMana();
        opponent.FillHandAndResetMana();
        StartFight();
    }

    public void StartFight()
    {
        StartCoroutine(Fight());
    }

    public void IsReady()
    {
        player.isReadyToStartRound = true;
        // DEBUG
        opponent.isReadyToStartRound = true;
    }

    private IEnumerator Fight()
    {
        player.isReadyToStartRound = false;
        opponent.isReadyToStartRound = false;

        // Wait for players
        yield return new WaitUntil(() => player.isReadyToStartRound && opponent.isReadyToStartRound);

        waitingPanel.SetActive(false);

        // IA plays (or online opp)
        enemyManager.Play();

        // Reveal summoned creatures this turn
        yield return turnManager.RevealCreaturesSummonedThisTurn();

        // Fight
        yield return battleFightManager.ProcessFights();

        // Reset at end turn
        player.FillHandAndResetMana();
        opponent.FillHandAndResetMana();

        // Move to next turn
        if (turnManager.Round < TurnManager.TOTAL_ROUND)
        {
            turnManager.MoveToNextRound();
            StartFight();
        }
    }
}
