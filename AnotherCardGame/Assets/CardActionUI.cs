using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CardActionUI : MonoBehaviour
{
    public static Action<CardActionUI> onCardActionSelect;

    public Image artworkImage;
    
    [HideInInspector] public int cardActionIndex;

    public void Select()
    {
        onCardActionSelect?.Invoke(this);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
