using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEffectChoicable
{
    public ChoiceDetails GetChoicesDetails();
    public void ChoicesSelectionCallback();
}

public class ChoiceDetails
{
    public ChoiceType choiceType;
    public int nbChoices;
    public string[] choicesBtnNames;
    public string validateBtnName = "Validate";
}

public enum ChoiceType { DroppableArea, Button }

