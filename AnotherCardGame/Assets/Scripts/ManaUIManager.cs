using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaUIManager : AbstractManager<ManaUIManager>
{
    public GameManager gameManager;
    public Transform manaTransform;

    protected override void Awake()
    {
        base.Awake();
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
