using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureBattlingUI : MonoBehaviour
{
    public Image artwork;
    public TextMeshProUGUI creatureNameTMP;
    public TextMeshProUGUI creaturePowerTMP;
    public TextMeshProUGUI creatureShieldTMP;

    public void SetCreature(CreatureUI creatureUI, bool flipArtwork = false)
    {
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
        creatureShieldTMP.text = creatureUI.stats.shield.ToString();
    }
}
