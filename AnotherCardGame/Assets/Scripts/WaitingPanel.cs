using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WaitingPanel : MonoBehaviour
{
    public TextMeshProUGUI waitingTMP;
    public float waitInterval = 0.6f;

    void OnEnable()
    {
        StopAllCoroutines();
        StartCoroutine(DisplayText());
    }

    private IEnumerator DisplayText()
    {
        while (true)
        {
            waitingTMP.text = "waiting";

            yield return new WaitForSeconds(waitInterval);
            
            waitingTMP.text = "waiting.";

            yield return new WaitForSeconds(waitInterval);

            waitingTMP.text = "waiting..";

            yield return new WaitForSeconds(waitInterval);

            waitingTMP.text = "waiting...";

            yield return new WaitForSeconds(waitInterval);
        }
    }
}
