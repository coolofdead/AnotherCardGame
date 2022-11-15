using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BattlefieldAreaManager : AbstractManager<BattlefieldAreaManager>
{
    public const int MAX_FIELD_AREA = 3;

    public Transform battlefieldTransform;

    public Transform playerBattlefield;
    public Transform opponentBattlefield;

    private DroppableAreaUI[] playerBattlefieldAreas;
    private DroppableAreaUI[] opponentBattlefieldAreas;

    protected override void Awake()
    {
        base.Awake();
        playerBattlefieldAreas = playerBattlefield.GetComponentsInChildren<DroppableAreaUI>();
        opponentBattlefieldAreas = opponentBattlefield.GetComponentsInChildren<DroppableAreaUI>();
    }

    public CreatureUI GetCreatureOnField(bool playerField, int fieldIndex)
    {
        DroppableAreaUI[] droppableAreaUIs = playerField ? playerBattlefieldAreas : opponentBattlefieldAreas;

        CreatureUI creatureUI = null;
        if (droppableAreaUIs[fieldIndex].ElemOnArea != null)
            creatureUI = droppableAreaUIs[fieldIndex].ElemOnArea.GetComponent<CreatureUI>();
        return creatureUI;
    }

    public DroppableAreaUI GetBattlefieldArea(bool playerField, int fieldIndex)
    {
        DroppableAreaUI[] droppableAreaUIs = playerField ? playerBattlefieldAreas : opponentBattlefieldAreas;

        return droppableAreaUIs[fieldIndex];
    }

    public CreatureUI[] GetAllCreaturesOnBattlefield()
    {
        return battlefieldTransform.GetComponentsInChildren<CreatureUI>();
    }

    public List<DroppableAreaUI> GetAllBattlefieldAreasAvailable(bool getPlayerSide)
    {
        List<DroppableAreaUI> availableBattlefieldAreas = new List<DroppableAreaUI>();
        for (int i = 0; i < MAX_FIELD_AREA; i++)
        {
            DroppableAreaUI droppableAreaUI = GetBattlefieldArea(getPlayerSide, i);
            if (droppableAreaUI.IsAvailable)
            {
                availableBattlefieldAreas.Add(droppableAreaUI);
            }
        }
        return availableBattlefieldAreas;
    }
}
