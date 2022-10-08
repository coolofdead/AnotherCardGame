using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardActionModulate
{
    public bool shouldModulatePower;
    [ConditionalField("shouldModulatePower", false)] public ModulationType powerModulationType;
    [ConditionalField("shouldModulatePower", false)] public int powerAmount;

    public bool shouldModulateNbHit;
    [ConditionalField("shouldModulateNbHit", false)] public ModulationType nbHitModulationType;
    [ConditionalField("shouldModulateNbHit", false)] public int nbHitAmount;

    public bool shouldModulateSpeed;
    [ConditionalField("shouldModulateSpeed", false)] public ModulationType speedModulationType;
    [ConditionalField("shouldModulateSpeed", false)] public int speedAmount;
}
