using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChoiceManager : AbstractManager<ChoiceManager>
{
    public GameObject choicesPanel;
    public Transform choicesTransform;
    public TextMeshProUGUI validateButtonTMP;

    public ChoiceDroppableArea choicesDroppablePrefab;

    private List<ChoiceDroppableArea> choiceDroppableAreas = new List<ChoiceDroppableArea>();
    private ChoicesDetails choicesDetails;

    public void SetupChoices(ChoicesDetails choicesDetails)
    {
        this.choicesDetails = choicesDetails;

        choicesPanel.SetActive(true);

        choicesTransform.DestroyAllChildren();

        choiceDroppableAreas.Clear();
        for (int i = 0; i < choicesDetails.nbChoices; i++)
        {
            ChoiceDroppableArea choiceDroppableArea = Instantiate(choicesDroppablePrefab, choicesTransform);
            choiceDroppableAreas.Add(choiceDroppableArea);
        }

        validateButtonTMP.text = choicesDetails.validateBtnName;
    }

    public void ValidateChoices()
    {
        List<DragableUI> choicesResults = new List<DragableUI>();
        foreach (ChoiceDroppableArea choiceDroppableArea in choiceDroppableAreas)
        {
            if (choiceDroppableArea.ElemOnArea != null)
                choicesResults.Add(choiceDroppableArea.ElemOnArea);
        }

        choicesDetails.callbackOnSelection(choicesResults);

        choicesPanel.SetActive(false);
    }
}
