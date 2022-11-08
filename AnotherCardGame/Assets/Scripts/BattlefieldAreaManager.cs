using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlefieldAreaManager : MonoBehaviour
{
    public static int MAX_FIELD_AREA = 3;

    public Transform playerBattlefield;
    public Transform opponentBattlefield;

    private DroppableAreaUI[] playerBattlefieldAreas;
    private DroppableAreaUI[] opponentBattlefieldAreas;

    private void Awake()
    {
        playerBattlefieldAreas = playerBattlefield.GetComponentsInChildren<DroppableAreaUI>();
        opponentBattlefieldAreas = opponentBattlefield.GetComponentsInChildren<DroppableAreaUI>();
    }

    public CreatureUI GetCreatureOnField(bool playerField, int fieldIndex)
    {
        DroppableAreaUI[] droppableAreaUIs = playerField ? playerBattlefieldAreas : opponentBattlefieldAreas;

        return droppableAreaUIs[fieldIndex].ElemOnArea?.GetComponent<CreatureUI>();
    }

    public DroppableAreaUI GetBattlefieldArea(bool playerField, int fieldIndex)
    {
        DroppableAreaUI[] droppableAreaUIs = playerField ? playerBattlefieldAreas : opponentBattlefieldAreas;

        return droppableAreaUIs[fieldIndex];
    }
}
