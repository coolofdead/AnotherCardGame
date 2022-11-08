using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUIManager : MonoBehaviour
{
    public GameManager gameManager;
    public Transform manaTransform;

    private void Awake()
    {
        gameManager.player.onManaChanged += UpdateManaUI;
    }

    private void UpdateManaUI(int currentManaLeft)
    {
        for (int i = 0; i < manaTransform.childCount; i++)
        {
            manaTransform.GetChild(i).gameObject.SetActive(i+1 <= currentManaLeft);
        }
    }

    private void OnDestroy()
    {
        gameManager.player.onManaChanged -= UpdateManaUI;
    }
}
