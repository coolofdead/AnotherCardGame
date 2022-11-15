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
    [HideInInspector] public CreatureStats Stats => BaseStats + persistentBonusStats + tempBonusStats;
    [HideInInspector] public CreatureStats BaseStats => creatureSO.stats;
    [HideInInspector] public CreatureStats persistentBonusStats;
    [HideInInspector] public CreatureStats tempBonusStats;
    [HideInInspector] public CreatureUI slayerCreatureUI;

    public List<AbstractCreatureEffect> CreatureEffects { get; private set; }

    public bool canAttackDirectly = true;

    private void Update()
    {
        powerTMP.text = Stats.power.ToString();
    }

    public void SetCreatureSO(CreatureSO creatureSO)
    {
        this.creatureSO = creatureSO;
        
        Init();
    }

    private void Init()
    {
        // Load stats from SO
        CreatureEffects = AbstractCreatureEffect.GenerateEffectsForCreature(creatureSO);
        foreach (AbstractCreatureEffect creatureEffect in CreatureEffects)
        {
            creatureEffect.creatureOwner = this;
        }

        // Setup UI from SO
        switch (creatureSO.creatureType)
        {
            case CreatureType.Fire:
                frameImage.color = fireFrameColor;
                frameParent = fireFrameParent;
                creatureTypeLogo = fireCreatureTypeLogo;
                break;
            case CreatureType.Plant:
                frameImage.color = plantFrameColor;
                frameParent = plantFrameParent;
                creatureTypeLogo = plantCreatureTypeLogo;
                break;
            case CreatureType.Water:
                frameImage.color = waterFrameColor;
                frameParent = waterFrameParent;
                creatureTypeLogo = waterCreatureTypeLogo;
                break;
            default:
                break;
        }

        foreach (AbstractCreatureEffect creatureEffect in CreatureEffects)
        {
            GameObject effectIconGo = Instantiate(effectIconPrefab, effectsIconTransform);
            effectIconGo.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = creatureEffect.IconSprite;
        }

        for (int i = 0; i < manaCostTransform.childCount; i++)
            manaCostTransform.GetChild(i).gameObject.SetActive(i+1 <= Stats.manaCost);

        artworkImage.sprite = creatureSO.artwork;
        artworkImage.transform.localPosition += (Vector3)creatureSO.artworkPositionOffset;
        artworkImage.transform.localScale = creatureSO.artworkScale;

        powerTMP.text = Stats.power.ToString();
    }

    public void CanBeSummon(bool isSummonable)
    {
        dragableUI.isEnabled = isSummonable;
    }

    public void Summon(float summonDelay)
    {
        // Register to effects
        GameEventManager.RegisterEffect(this);

        CanBeSummon(false);

        Invoke("HideCreatureTypeThenShowFrame", summonDelay);
    }

    public void HideAndLock()
    {
        dragableUI.isEnabled = false;

        creatureTypeLogo.SetActive(true);
        contentParent.SetActive(false);
    }

    private void HideCreatureTypeThenShowFrame()
    {
        creatureTypeLogo.SetActive(false);
        frameParent.SetActive(true);
        contentParent.SetActive(true);
    }

    public void ResetTempStats()
    {
        tempBonusStats = new CreatureStats();
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

    private void OnDestroy()
    {
        // Unscubscribe to effects
        GameEventManager.UnregisterEffect(this);
    }
}
