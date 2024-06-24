using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DifficultySettingsElement : MonoBehaviour
{
    [SerializeField] TMP_Text label;
    [SerializeField] TMP_Text value;
    [SerializeField] Slider slider;
    [SerializeField] DifficultyObject difficulty;

    private void Start()
    {
        InitDisplay();
    }

    [ContextMenu("Refresh Difficulty Display")]
    public void InitDisplay()
    {
        slider.onValueChanged.AddListener(delegate { difficulty.SetValue(slider.value); });
        label.text = difficulty.Label;
        value.text = difficulty.FinalValue.ToString("00.0");
    }

    // Update is called once per frame
    void Update()
    {
        value.text = difficulty.FinalValue.ToString();
    }
}
