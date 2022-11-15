using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageCounterManager : AbstractManager<DamageCounterManager>
{
    public GameManager gameManager;

    public TextMeshProUGUI playerDamageCounterTMP;
    public TextMeshProUGUI opponentDamageCounterTMP;

    protected override void Awake()
    {
        base.Awake();
        gameManager.player.onDamageReceived += UpdateDamageCounter;
        gameManager.opponent.onDamageReceived += UpdateDamageCounter;
    }

    private void UpdateDamageCounter(Player player)
    {
        TextMeshProUGUI damageCounterTMP = gameManager.player == player ? playerDamageCounterTMP : opponentDamageCounterTMP;
        damageCounterTMP.text = player.DamageCounter.ToString();
    }

    private void OnDestroy()
    {
        gameManager.player.onDamageReceived -= UpdateDamageCounter;
        gameManager.opponent.onDamageReceived -= UpdateDamageCounter;
    }
}
