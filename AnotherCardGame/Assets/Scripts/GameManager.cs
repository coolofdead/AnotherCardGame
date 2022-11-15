using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : AbstractManager<GameManager>
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

    // Actions
    public Action<GameState> onGameStateChanged;

    private void Start()
    {
        // DEBUG
        player.deck.Shuffle();
        opponent.deck.Shuffle();

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

        onGameStateChanged?.Invoke(GameState.PlaceCreatures);

        // Wait for players
        yield return new WaitUntil(() => player.isReadyToStartRound && opponent.isReadyToStartRound);

        waitingPanel.SetActive(false);

        // IA plays (or online opp)
        enemyManager.Play();

        onGameStateChanged?.Invoke(GameState.SummonCreatures);

        // Reveal summoned creatures this turn
        yield return turnManager.RevealCreaturesSummonedThisTurn();

        onGameStateChanged?.Invoke(GameState.ProcessFight);

        // Fight
        yield return battleFightManager.ProcessFights();

        onGameStateChanged?.Invoke(GameState.FinishTurn);

        // Move to next turn
        if (turnManager.Round < TurnManager.TOTAL_ROUND)
        {
            yield return turnManager.MoveToNextRound();

            // Reset at end turn
            player.FillHandAndResetMana();
            opponent.FillHandAndResetMana();

            StartCoroutine(Fight());
        }
    }
}
