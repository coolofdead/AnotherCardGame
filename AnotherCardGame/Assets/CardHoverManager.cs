using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardHoverManager : AbstractManager<CardHoverManager>
{
    public GameObject cardDescriptionParent;

    public Image cardArtworkImage;
    public TextMeshProUGUI cardNameTMP;
    public TextMeshProUGUI cardPowerTMP;

    public Transform cardDescParent;
    public Transform cardEffectDescPrefab;

    protected override void Awake()
    {
        base.Awake();
        CreatureUI.onCardHoverEnter += ShowHoveredCard;
        CreatureUI.onCardHoverExit += HideHoveredCard;
    }

    private void ShowHoveredCard(CreatureUI creatureUI)
    {
        cardDescriptionParent.SetActive(true);

        cardArtworkImage.sprite = creatureUI.creatureSO.artwork;
        cardNameTMP.text = creatureUI.creatureSO.creatureName;
        cardPowerTMP.text = creatureUI.Stats.power.ToString();

        cardDescParent.DestroyAllChildren();

        foreach (AbstractCreatureEffect creatureEffect in creatureUI.CreatureEffects)
        {
            Transform newCardEffectDesc = Instantiate(cardEffectDescPrefab, cardDescParent);
            newCardEffectDesc.GetChild(0).GetComponent<Image>().sprite = creatureEffect.IconSprite;
            newCardEffectDesc.GetChild(1).GetComponent<TextMeshProUGUI>().text = creatureEffect.GetEffectDescription();
        }
    }

    private void HideHoveredCard(CreatureUI creatureUI)
    {
        cardDescriptionParent.SetActive(false);
    }

    private void OnDestroy()
    {
        CreatureUI.onCardHoverEnter -= ShowHoveredCard;
        CreatureUI.onCardHoverExit -= HideHoveredCard;
    }
}
