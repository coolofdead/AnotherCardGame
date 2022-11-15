using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ChoicesDetails
{
    public ChoicesType choiceType;
    public int nbChoices;
    public string[] choicesBtnNames;
    public string validateBtnName = "Validate";
    public Action<List<DragableUI>> callbackOnSelection;
}
