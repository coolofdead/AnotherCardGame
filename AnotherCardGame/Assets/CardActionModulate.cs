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

    public void Modulate(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CardActionTargetType target)
    {
        if (shouldModulatePower)
        {
            ModulatePower(playerCreature, opponentCreature, target);
        }

        if (shouldModulateNbHit)
        {
            ModulateNbHit(playerCreature, opponentCreature, target);
        }

        if (shouldModulateSpeed)
        {
            ModulateSpeed(playerCreature, opponentCreature, target);
        }
    }

    private void  ModulatePower(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CardActionTargetType target)
    {
        if (target == CardActionTargetType.Self || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    playerCreature.power += powerAmount;
                    break;
                case ModulationType.Substract:
                    playerCreature.power -= powerAmount;
                    break;
                default:
                    break;
            }
        }

        if (target == CardActionTargetType.Opponent || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    opponentCreature.power += powerAmount;
                    break;
                case ModulationType.Substract:
                    opponentCreature.power -= powerAmount;
                    break;
                default:
                    break;
            }
        }
    }

    private void ModulateNbHit(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CardActionTargetType target)
    {
        if (target == CardActionTargetType.Self || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    playerCreature.nbHit += nbHitAmount;
                    break;
                case ModulationType.Substract:
                    playerCreature.nbHit -= nbHitAmount;
                    break;
                default:
                    break;
            }
        }

        if (target == CardActionTargetType.Opponent || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    opponentCreature.nbHit += nbHitAmount;
                    break;
                case ModulationType.Substract:
                    opponentCreature.nbHit -= nbHitAmount;
                    break;
                default:
                    break;
            }
        }
    }

    private void ModulateSpeed(CreatureFightingUI playerCreature, CreatureFightingUI opponentCreature, CardActionTargetType target)
    {
        if (target == CardActionTargetType.Self || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    playerCreature.speed += speedAmount;
                    break;
                case ModulationType.Substract:
                    playerCreature.speed -= speedAmount;
                    break;
                default:
                    break;
            }
        }

        if (target == CardActionTargetType.Opponent || target == CardActionTargetType.Both)
        {
            switch (powerModulationType)
            {
                case ModulationType.Add:
                    opponentCreature.speed += speedAmount;
                    break;
                case ModulationType.Substract:
                    opponentCreature.speed -= speedAmount;
                    break;
                default:
                    break;
            }
        }
    }
}
