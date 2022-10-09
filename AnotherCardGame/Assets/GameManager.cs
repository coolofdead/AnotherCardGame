using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    private const int MAX_CARD_ACTION_TO_SELECT = 3;
    private const int NB_FIGHT_ROUND = 3;

    [Header("Players")]
    public Player player;
    public Player opponent;

    public List<int> playerCardActionSelected = new List<int>();

    [Header("UI")]
    public CreatureFightingUI playerCreatureFighting;
    public CreatureFightingUI opponentCreatureFighting;
    public Transform actionCardsListTransform;
    public Transform actionCardsSelectedTransform;
    public TextMeshProUGUI cardActionDescriptionTMP;
    public float scrollBarCardValue = 0.086f;
    public GameObject startFightButton;
    public TextMeshProUGUI scoreTMP;
    public TextMeshProUGUI playerDeckTMP;
    public TextMeshProUGUI opponnentDeckTMP;
    public GameObject endScreenPanel;
    public TextMeshProUGUI victoryTMP;

    private int currentFight = 0;
    private int nbPlayerFightWon = 0;
    private int nbOpponentFightWon = 0;

    private void Awake()
    {
        CardActionUI.onCardActionSelect += PickCardAction;
    }

    private void Start()
    {
        player.deck.Shuffle();
        opponent.deck.Shuffle();

        SetupNextFight();
        SetupCardActionsUI();
    }

    public void LoadFightScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void StartFight()
    {
        StartCoroutine(Fight());
    }

    private IEnumerator Fight()
    {
        for (int i = 0; i < NB_FIGHT_ROUND; i++)
        {
            CreatureFightingUI firstAttacker = playerCreatureFighting.speed >= opponentCreatureFighting.speed ? playerCreatureFighting : opponentCreatureFighting;
            CreatureFightingUI targetCreature = firstAttacker == playerCreatureFighting ? opponentCreatureFighting : playerCreatureFighting;

            // Activate action card
            CardActionSO playerCard = player.deck.cardActions[playerCardActionSelected[0]];
            if (playerCard.requireMinPower == false || playerCreatureFighting.power > playerCard.minPowerRequired)
            {
                if (playerCard.hasModulation)
                {
                    playerCard.modulation.Modulate(playerCreatureFighting, opponentCreatureFighting, playerCard.target);
                }

                RemoveCardAction(0);
            }

            CardActionSO opponentCard = opponent.deck.cardActions[currentFight * NB_FIGHT_ROUND + i];
            if (opponentCard.requireMinPower == false || opponentCreatureFighting.power > opponentCard.minPowerRequired)
            {
                if (opponentCard.hasModulation)
                {
                    opponentCard.modulation.Modulate(opponentCreatureFighting, playerCreatureFighting, opponentCard.target);
                }
            }

            for (int attack = 0; attack < firstAttacker.nbHit; attack++)
            {
                targetCreature.TakeDamage(firstAttacker.power);
            }

            yield return new WaitForSeconds(2);

            // Reverse
            for (int attack = 0; attack < targetCreature.nbHit; attack++)
            {
                firstAttacker.TakeDamage(targetCreature.power);
            }

            yield return new WaitForSeconds(2);
        }

        CreatureFightingUI winnerCreature = playerCreatureFighting.damage < opponentCreatureFighting.damage ? playerCreatureFighting : opponentCreatureFighting;
        winnerCreature = playerCreatureFighting.damage == opponentCreatureFighting.damage ? null : winnerCreature;
        if (winnerCreature == playerCreatureFighting)
            nbPlayerFightWon++;
        if (winnerCreature == opponentCreatureFighting)
            nbOpponentFightWon++;

        scoreTMP.text = nbOpponentFightWon + "<br>------<br>" + nbPlayerFightWon;

        playerCreatureFighting.ResetDamage();
        opponentCreatureFighting.ResetDamage();

        currentFight++;
        SetupNextFight();

        playerDeckTMP.text = (player.deck.creatures.Length - currentFight).ToString();
        opponnentDeckTMP.text = (player.deck.creatures.Length - currentFight).ToString();

        if (currentFight == 4)
        {
            endScreenPanel.SetActive(true);
            if (nbPlayerFightWon > nbOpponentFightWon)
            {
                victoryTMP.text = "Player won";
            }

            if (nbPlayerFightWon < nbOpponentFightWon)
            {
                victoryTMP.text = "Bot won";
            }

            if (nbPlayerFightWon == nbOpponentFightWon)
            {
                victoryTMP.text = "Draw";
            }
        }
    }

    private void SetupNextFight()
    {
        playerCreatureFighting.SetCreatureSO(player.deck.creatures[currentFight]);
        opponentCreatureFighting.SetCreatureSO(opponent.deck.creatures[currentFight]);
    }

    private void SetupCardActionsUI()
    {
        CardActionUI[] cardActionUIs = actionCardsListTransform.GetComponentsInChildren<CardActionUI>();
        for (int i = 0; i < cardActionUIs.Length; i++)
        {
            cardActionUIs[i].artworkImage.sprite = player.deck.cardActions[i].artwork;
            cardActionUIs[i].cardActionIndex = i;
        }
    }

    private void PickCardAction(CardActionUI cardActionSelected)
    {
        if (playerCardActionSelected.Count >= MAX_CARD_ACTION_TO_SELECT)
            return;

        if (player.deck.cardActions[cardActionSelected.cardActionIndex].creatureType != playerCreatureFighting.creatureSO.creatureType)
            return;

        playerCardActionSelected.Add(cardActionSelected.cardActionIndex);
        cardActionSelected.Hide();

        actionCardsSelectedTransform.GetChild(playerCardActionSelected.Count - 1).GetChild(1).gameObject.SetActive(true);

        Sprite artworkCardActionSelected = player.deck.cardActions[cardActionSelected.cardActionIndex].artwork;
        Image artworkCardActionUISelected = actionCardsSelectedTransform.GetChild(playerCardActionSelected.Count - 1).GetChild(1).GetChild(0).GetComponent<Image>();
        artworkCardActionUISelected.sprite = artworkCardActionSelected;

        if (playerCardActionSelected.Count == MAX_CARD_ACTION_TO_SELECT)
            startFightButton.SetActive(true);
    }

    public void RemoveCardAction(int cardActionIndex)
    {
        if (cardActionIndex >= playerCardActionSelected.Count)
            return;

        int cardActionUIIndex = playerCardActionSelected[cardActionIndex];
        CardActionUI cardActionUIToEnable = actionCardsListTransform.GetChild(cardActionUIIndex).GetComponent<CardActionUI>();
        cardActionUIToEnable.Show();

        playerCardActionSelected.RemoveAt(cardActionIndex);

        for (int i = 0; i < 3; i++)
        {
            actionCardsSelectedTransform.GetChild(i).GetChild(1).gameObject.SetActive(i < playerCardActionSelected.Count);

            if (i >= playerCardActionSelected.Count)
                continue;

            Sprite artworkCardActionSelected = player.deck.cardActions[playerCardActionSelected[i]].artwork;
            Image artworkCardActionUISelected = actionCardsSelectedTransform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Image>();
            artworkCardActionUISelected.sprite = artworkCardActionSelected;
        }

        startFightButton.SetActive(playerCardActionSelected.Count == MAX_CARD_ACTION_TO_SELECT);
    }

    public void OnCardActionScroll(Scrollbar scrollbar)
    {
        int cardActionIndexHovering = (int)(scrollbar.value / scrollBarCardValue);

        if (cardActionIndexHovering < 0)
            return;

        cardActionDescriptionTMP.text = player.deck.cardActions[cardActionIndexHovering].description;
    }

    private void OnDestroy()
    {
        CardActionUI.onCardActionSelect -= PickCardAction;
    }
}
