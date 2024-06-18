using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StepsHand : MonoBehaviour
{
    private int totalSteps;
    private int stepCompleted = 0;
    private List<string> stars = new();

    public TMP_Text handStarText;
    public TMP_Text handText;

    // Start is called before the first frame update
    void Start()
    {
        totalSteps = GameManager.Instance.totalSteps;
        for (int i = 0; i < totalSteps; i++)
        {
            stars.Add("☆");
        }
        SetText();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.StepCompleted > stepCompleted)
        {
            stepCompleted = GameManager.Instance.StepCompleted;
            stars[stepCompleted - 1] = "★";
            SetText();
        }
    }

    private void SetText()
    {
        handStarText.text = string.Join(" ", stars);
        if(GameManager.Instance.stepsObjective.Length > 0)
        {
            try
            {
                handText.text = GameManager.Instance.stepsObjective[stepCompleted];
            }
            catch
            {
                handText.text = GameManager.Instance.stepsObjective[^1];
            }
        }
    }
}
