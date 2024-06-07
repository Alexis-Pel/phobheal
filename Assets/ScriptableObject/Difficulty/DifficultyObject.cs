using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Difficulty", menuName = "Phobia/New Difficulty")]
public class DifficultyObject : ScriptableObject
{
    // Info
    public string Label;
    public float InitValue;

    public float FinalValue;

    public void SetValue(float coeff)
    {
        FinalValue = InitValue + InitValue * coeff;
    }
}