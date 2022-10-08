using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Action", menuName = "Card Action/New Card Action", order = 2)]
public class CardActionSO : ScriptableObject
{
    [Header("Settings")]
    public CardActionTargetType target = CardActionTargetType.None;
    public CreatureType creatureType;

    [Header("Info")]
    public string cardActionName;
    public string description;
    public Sprite artwork;

    public bool hasModulation;
    [ConditionalField("hasModulation", false)] public CardActionModulate modulation;
}
