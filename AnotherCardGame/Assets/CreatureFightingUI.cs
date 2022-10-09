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

    public TextMeshProUGUI damageTMP;
    public TextMeshProUGUI powerTMP;
    public TextMeshProUGUI nbHitTMP;
    public TextMeshProUGUI speedTMP;
    public Image artworkImage;
    public Image frameImage;

    [HideInInspector] public int power;
    [HideInInspector] public int nbHit;
    [HideInInspector] public int speed;
    [HideInInspector] public int damage;

    [HideInInspector] public CreatureSO creatureSO;

    public void SetCreatureSO(CreatureSO creatureSO)
    {
        this.creatureSO = creatureSO;
        power = creatureSO.power;
        nbHit = creatureSO.nbHit;
        speed = creatureSO.speed;
        damage = 0;
    }

    void Update()
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
        powerTMP.text = power.ToString();
        nbHitTMP.text = nbHit.ToString();
        speedTMP.text = speed.ToString();
        artworkImage.sprite = creatureSO.artwork;
    }

    public void TakeDamage(int amount)
    {
        damage += amount;
        damageTMP.text = damage.ToString();
    }

    public  void ResetDamage()
    {
        damage = 0;
        damageTMP.text = "";
    }
}
