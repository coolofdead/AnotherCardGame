using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreaturePowerText : MonoBehaviour
{
    public float timeInSecToUpdatePowerText = 0.7f;

    public TextMeshProUGUI powerTMP;
    public TextMeshProUGUI shieldTMP;
    private Color shieldColor;
    private Vector2 defaultLocalPos;

    public CreatureBattlingUI creatureBattlingUI;

    private void Awake()
    {
        shieldColor = shieldTMP.color;
        defaultLocalPos = shieldTMP.transform.localPosition;
    }

    public void DisplayPowerWithShield()
    {
        StartCoroutine(DisplayDefensivePower());
    }

    private IEnumerator DisplayDefensivePower()
    {
        int power = creatureBattlingUI.CreatureUI.Stats.power;
        int shield = creatureBattlingUI.CreatureUI.Stats.shield;

        float currentTime = 0;


        while (currentTime < timeInSecToUpdatePowerText)
        {
            currentTime += Time.deltaTime;

            float percentage = Mathf.Min(currentTime / timeInSecToUpdatePowerText, 1);

            int currentShieldValue = (int)Mathf.Max(shield - shield * percentage, 0);

            powerTMP.text = ((int)(power + shield * percentage)).ToString();
            shieldTMP.text = currentShieldValue > 0 ? currentShieldValue.ToString() : "";

            powerTMP.color = Color.Lerp(Color.white, shieldColor, percentage);

            yield return null;
        }
    }

    public void ResetTexts()
    {
        powerTMP.color = Color.white;
        powerTMP.text = creatureBattlingUI.CreatureUI.Stats.power.ToString();
        shieldTMP.transform.localPosition = defaultLocalPos;
    }
}
