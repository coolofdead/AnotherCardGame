using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System;

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

    private void Start()
    {
        player.Init();
        opponent.Init();

        // DEBUG
        StartFight();
    }

    public void StartFight()
    {
        StartCoroutine(Fight());
    }

    public void IsReady()
    {
        player.isReadyToStartRound = true;
    }

    private IEnumerator Fight()
    {
        print("wait for players");

        // Wait for both players to be ready
        yield return new WaitUntil(() => player.isReadyToStartRound && opponent.isReadyToStartRound);

        print("players ready");

        waitingPanel.SetActive(false);

        enemyManager.Play();

        yield return turnManager.RevealCreaturesSummonedThisTurn();

        print("start battle");

        // Battle
        for (int i = 0; i < BattlefieldAreaManager.MAX_FIELD_AREA; i++)
        {
            CreatureUI playerCreatureUI = battlefieldAreaManager.GetCreatureOnField(true, i);
            CreatureUI opponentCreatureUI = battlefieldAreaManager.GetCreatureOnField(false, i);

            if (playerCreatureUI == null || opponentCreatureUI == null)
            {
                // Add logic here
                print("direct damage from one side");
                continue;
            }

            yield return battleManager.Fight(playerCreatureUI, opponentCreatureUI);
        }

        // Draw to fill hand

        // Move one round forward

        // Loop


        //CreatureFightingUI firstAttacker = playerCreatureFighting.stats.speed >= opponentCreatureFighting.stats.speed ? playerCreatureFighting : opponentCreatureFighting;
        //CreatureFightingUI targetCreature = firstAttacker == playerCreatureFighting ? opponentCreatureFighting : playerCreatureFighting;

        //GameEventManager.TriggerEvent(EffectActivationTimeType.StartRound, new EventStruct());

        //// First attacker attacks
        //for (int attack = 0; attack < firstAttacker.stats.nbHit; attack++)
        //{
        //    bool hasTakenDamage = targetCreature.TakeDamage(firstAttacker.stats.power);
        //    if (hasTakenDamage)
        //    {
        //        EffectActivationTimeType damageEventType = firstAttacker == playerCreatureFighting ? EffectActivationTimeType.OnDamageDeal : EffectActivationTimeType.OnDamageReceived;
        //        GameEventManager.TriggerEvent(damageEventType, new EventStruct());
        //    }
        //}

        //// Target attacks
        //for (int attack = 0; attack < targetCreature.stats.nbHit; attack++)
        //{
        //    firstAttacker.TakeDamage(targetCreature.stats.power);
        //}

        //yield return new WaitForSeconds(5);

        //GameEventManager.TriggerEvent(EffectActivationTimeType.EndRound, new EventStruct());


        //CreatureFightingUI winnerCreature = playerCreatureFighting.damage < opponentCreatureFighting.damage ? playerCreatureFighting : opponentCreatureFighting;
        //winnerCreature = playerCreatureFighting.damage == opponentCreatureFighting.damage ? null : winnerCreature;
        //if (winnerCreature == playerCreatureFighting)
        //    nbPlayerFightWon++;
        //if (winnerCreature == opponentCreatureFighting)
        //    nbOpponentFightWon++;

        //scoreTMP.text = nbOpponentFightWon + "<br>------<br>" + nbPlayerFightWon;

        //playerCreatureFighting.ResetDamage();
        //opponentCreatureFighting.ResetDamage();

        //currentFight++;
        //SetupNextFight();

        //GameEventManager.TriggerEvent(EffectActivationTimeType.OnEndFight, new EventStruct());

        //playerDeckTMP.text = (player.deck.creatures.Length - currentFight).ToString();
        //opponnentDeckTMP.text = (player.deck.creatures.Length - currentFight).ToString();

        //if (currentFight == 4)
        //{
        //    endScreenPanel.SetActive(true);
        //    if (nbPlayerFightWon > nbOpponentFightWon)
        //    {
        //        victoryTMP.text = "Player won";
        //    }

        //    if (nbPlayerFightWon < nbOpponentFightWon)
        //    {
        //        victoryTMP.text = "Bot won";
        //    }

        //    if (nbPlayerFightWon == nbOpponentFightWon)
        //    {
        //        victoryTMP.text = "Draw";
        //    }
        //}
    }


    private void DisplayCreatureSupportEffect(int creatureSupportIndex)
    {
        //cardActionDescriptionTMP.text = player.deck.creatures[creatureSupportIndex].effectDescription;
    }
}
