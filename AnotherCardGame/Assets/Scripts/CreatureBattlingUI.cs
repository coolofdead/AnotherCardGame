using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;

public class CreatureBattlingUI : MonoBehaviour
{
    public Image artwork;
    public TextMeshProUGUI creatureNameTMP;
    public TextMeshProUGUI creaturePowerTMP;
    public TextMeshProUGUI creatureShieldTMP;

    public GameObject playerFightingParent;
    public Transform attackAnimationParent;

    public PlayableDirector shieldDefenseTimeline;

    public bool DoneDoingAnimations { get; private set; }

    public CreatureUI CreatureUI { get; private set; }

    public void SetCreature(CreatureUI creatureUI, bool flipArtwork = false)
    {
        DoneDoingAnimations = true;

        playerFightingParent.SetActive(creatureUI == null);
        creatureNameTMP.text = "";
        creaturePowerTMP.text = "";
        if (creatureUI == null) 
            return;

        CreatureUI = creatureUI;
        artwork.sprite = creatureUI.creatureSO.artwork;

        if (creatureUI.creatureSO.battleArtworkAnchoredPosition != Vector2.zero)
            artwork.rectTransform.anchoredPosition = creatureUI.creatureSO.battleArtworkAnchoredPosition;

        if (creatureUI.creatureSO.battleArtworkScale != Vector2.zero)
            artwork.rectTransform.localScale = creatureUI.creatureSO.battleArtworkScale;

        if (creatureUI.creatureSO.flipArtworkOnBattle)
            artwork.rectTransform.Rotate(Vector3.up * 180, Space.Self);

        if (flipArtwork)
            artwork.rectTransform.Rotate(Vector3.up * 180, Space.Self);

        creatureNameTMP.text = creatureUI.creatureSO.creatureName;
        creaturePowerTMP.text = creatureUI.stats.power.ToString();
        creatureShieldTMP.text = creatureUI.stats.shield > 0 ? creatureUI.stats.shield.ToString() : "";
    }

    public void SurvivesWithShield()
    {
        DoneDoingAnimations = false;
        
        shieldDefenseTimeline.Play();
    }

    public void KillOpponent(CreatureBattlingUI creatureBattlingUI)
    {
        DoneDoingAnimations = false;

        CreatureAttackAnimationHandler creatureAttackAnimationHandler = Instantiate(CreatureUI.creatureSO.attackAnimationPrefab, creatureBattlingUI.attackAnimationParent);
        creatureAttackAnimationHandler.onAttackAnimationFinished += BattleAnimationFinished;
        creatureAttackAnimationHandler.PlayAttackAnimation();
    }

    public void BattleAnimationFinished()
    {
        DoneDoingAnimations = true;
    }
}
