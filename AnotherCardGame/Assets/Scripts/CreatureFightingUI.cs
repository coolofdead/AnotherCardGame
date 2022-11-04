using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreatureFightingUI : MonoBehaviour
{
    public Color plantFrameColor;
    public Color fireFrameColor;
    public Color waterFrameColor;

    public TextMeshProUGUI powerTMP;
    public TextMeshProUGUI shieldTMP;
    public Image artworkImage;
    public Image frameImage;

    public int damage;

    [HideInInspector] public CreatureStats stats;

    [HideInInspector] public CreatureSO creatureSO;

    public void SetCreatureSO(CreatureSO creatureSO)
    {
        this.creatureSO = creatureSO;
        stats = creatureSO.stats.Clone();
        damage = 0;
        artworkImage.transform.localPosition += (Vector3)creatureSO.artworkPositionOffset;
        artworkImage.transform.localScale = (Vector3)creatureSO.artworkScale;
    }

    void FixedUpdate()
    {
        if (creatureSO == null)
            return;

        switch (creatureSO.creatureType)
        {
            case CreatureType.Fire:
                frameImage.color = fireFrameColor;
                break;
            case CreatureType.Plant:
                frameImage.color = plantFrameColor;
                break;
            case CreatureType.Water:
                frameImage.color = waterFrameColor;
                break;
            default:
                break;
        }
        powerTMP.text = stats.power.ToString();
        shieldTMP.text = stats.shield > 0 ? stats.shield.ToString() : "";
        artworkImage.sprite = creatureSO.artwork;
    }

    public bool TakeDamage(int amount)
    {
        int damageLeft = Mathf.Abs(amount - stats.shield);

        stats.shield = damageLeft > 0 ? 0 : stats.shield;
        //shieldTMP.text = stats.shield <= 0 ? "" : stats.shield.ToString();

        if (damageLeft <= 0)
            return false;

        damage += damageLeft;

        return true;
    }

    public  void ResetDamage()
    {
        damage = 0;
        stats.shield = 0;
        shieldTMP.text = "";
    }
}
