using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature", menuName = "Creature/New Creature", order = 1)]
public class CreatureSO : ScriptableObject
{
    [Header("Creature Stats")]
    public CreatureType creatureType;
    [Range(0, 10000)]
    public int power;
    [Range(1, 9)]
    public int nbHit;
    [Range(1, 9)]
    public int speed;

    [Header("Info")]
    public string creatureName;
    public Sprite artwork;
}
