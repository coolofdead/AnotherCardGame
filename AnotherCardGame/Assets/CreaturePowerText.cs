using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreaturePowerText : MonoBehaviour
{
    public TextMeshProUGUI powerTMP;
    public TextMeshProUGUI shieldTMP;
    public Color shieldColor;

    public void DisplayPowerWithShield(CreatureUI creatureUI)
    {
        //StartCoroutine(DisplayDefensivePower(creatureUI));
    }

    private IEnumerator DisplayDefensivePower(CreatureUI creatureUI)
    {
        yield return null;

        //int power = 3000;
        //int shield = 3000;

        //float currentTime = 0;

        //for (int i = 0; i <= shield; i++)
        //{
        //    currentTime += Time.deltaTime;

        //    powerTMP.text = (power + i).ToString();
        //    shieldTMP.text = (shield - i).ToString();

        //    powerTMP.color = Color.Lerp(Color.white, targetColor, 0.2f * Time.deltaTime);

        //    float delay = 1 / (480 + i);
        //    yield return new WaitForSeconds(delay);
        //}
    }
}
