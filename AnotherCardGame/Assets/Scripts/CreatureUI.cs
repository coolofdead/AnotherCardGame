using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureUI : MonoBehaviour
{
    public Color plantFrameColor;
    public Color fireFrameColor;
    public Color waterFrameColor;

    public Transform manaCostTransform;
    public Transform effectsIconTransform;
    public GameObject effectIconPrefab;
    public TextMeshProUGUI powerTMP;
    public Image artworkImage;
    public Image frameImage;
    public GameObject plantFrameParent;
    public GameObject fireFrameParent;
    public GameObject waterFrameParent;
    private GameObject frameParent;
    public GameObject fireCreatureTypeLogo;
    public GameObject waterCreatureTypeLogo;
    public GameObject plantCreatureTypeLogo;
    private GameObject creatureTypeLogo;
    public GameObject contentParent;
    public Image fireDeathAnimation;
    public Image plantDeathAnimation;
    public Image waterDeathAnimation;

    public DragableUI dragableUI;

    [HideInInspector] public CreatureSO creatureSO;
    [HideInInspector] public CreatureStats stats;
    [HideInInspector] public CreatureUI slayerCreatureUI;

    private bool hasBeenSummoned;

    public void SetCreatureSO(CreatureSO creatureSO)
    {
        this.creatureSO = creatureSO;
        this.stats = creatureSO.stats.Clone();
        
        Init();
    }

    private void Init()
    {
        switch (creatureSO.creatureType)
        {
            case CreatureType.Fire:
                frameImage.color = fireFrameColor;
                frameParent = fireFrameParent;
                creatureTypeLogo = fireCreatureTypeLogo;
                //fireFrameParent.SetActive(true);
                break;
            case CreatureType.Plant:
                frameImage.color = plantFrameColor;
                frameParent = plantFrameParent;
                creatureTypeLogo = plantCreatureTypeLogo;
                //plantFrameParent.SetActive(true);
                break;
            case CreatureType.Water:
                frameImage.color = waterFrameColor;
                frameParent = waterFrameParent;
                creatureTypeLogo = waterCreatureTypeLogo;
                //waterFrameParent.SetActive(true);
                break;
            default:
                break;
        }

        foreach (AbstractCreatureEffect creatureEffect in creatureSO.creatureEffects)
        {
            GameObject effectIconGo = Instantiate(effectIconPrefab, effectsIconTransform);
            effectIconGo.transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = creatureEffect.IconSprite;
        }

        for (int i = 0; i < manaCostTransform.childCount; i++)
            manaCostTransform.GetChild(i).gameObject.SetActive(i+1 <= stats.manaCost);

        artworkImage.sprite = creatureSO.artwork;
        artworkImage.transform.localPosition += (Vector3)creatureSO.artworkPositionOffset;
        artworkImage.transform.localScale = creatureSO.artworkScale;

        powerTMP.text = stats.power.ToString();
        //shieldTMP.text = stats.shield > 0 ? stats.shield.ToString() : "";
    }

    public void CanBeSummoned(bool isSummonable)
    {
        // Refacto => Summon from hand != move on board

        dragableUI.isEnabled = isSummonable && hasBeenSummoned == false;
    }

    public void Summon(float summonDelay)
    {
        hasBeenSummoned = true;
        CanBeSummoned(false);

        Invoke("HideCreatureTypeThenShowFrame", summonDelay);
    }

    public void Hide()
    {
        creatureTypeLogo.SetActive(true);
        contentParent.SetActive(false);
    }

    private void HideCreatureTypeThenShowFrame()
    {
        creatureTypeLogo.SetActive(false);
        frameParent.SetActive(true);
        contentParent.SetActive(true);
    }

    public void ShowDeath()
    {
        Image deathEffectImage = null;
        if (slayerCreatureUI.creatureSO.creatureType == CreatureType.Fire)
            deathEffectImage = fireDeathAnimation;
        if (slayerCreatureUI.creatureSO.creatureType == CreatureType.Plant)
            deathEffectImage = plantDeathAnimation;
        if (slayerCreatureUI.creatureSO.creatureType == CreatureType.Water)
            deathEffectImage = waterDeathAnimation;

        StartCoroutine(PlayDeathAnimation(deathEffectImage));
    }

    private IEnumerator PlayDeathAnimation(Image effectImage)
    {
        effectImage.gameObject.SetActive(true);

        float timeOfAnimation = 4f;
        float currentTime = 0f;
        Color targetColor = effectImage.color;

        while (currentTime < timeOfAnimation)
        {
            currentTime += Time.deltaTime;

            effectImage.color = Color.Lerp(Color.white, targetColor, currentTime / timeOfAnimation);

            yield return null;
        }

        Destroy(gameObject);
    }
}
