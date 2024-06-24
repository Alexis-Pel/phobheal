using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private bool debug = false;

    [SerializeField] private GoalElevator cage;
    [SerializeField] private FishSpawn seaFloor;

    [SerializeField] private DifficultyObject fog;
    [SerializeField] private DifficultyObject depth;
    [SerializeField] private DifficultyObject faunaDensity;

    [SerializeField] private DifficultyObject height;
    [SerializeField] private DifficultyObject opacity;
    [SerializeField] private Material cageMaterial;

    public bool IsValidate { get; private set; }

    private void Start()
    {
        if (debug) ValidDifficulty();
        cageMaterial.SetColor("_Color", new Color32(143, 177, 207, 255));
    }

    public void ValidDifficulty()
    {

        #region playerObjective
        if (seaFloor != null)
        {
            cage.HeightGoal = depth.FinalValue;
            seaFloor.transform.position = new Vector3(seaFloor.transform.position.x, depth.FinalValue, seaFloor.transform.position.z);
        };
        #endregion

        #region fogDistance
        if(fog != null)
        {
            RenderSettings.fogEndDistance = fog.FinalValue;
        }
        #endregion

        #region FaunaDensity
        if(faunaDensity != null)
        {
            seaFloor.StartSpawn(faunaDensity.FinalValue);
        }
        #endregion

        #region playerHeight
        if(height != null)
        {
            cage.HeightGoal = Mathf.Clamp((height.FinalValue * 10) + cage.heightStart, 85, 1215);
        }
        #endregion

        #region cageOpacity
        if (opacity != null)
        {
            cageMaterial.SetColor("_Color", new Color32(143, 177, 207, (byte)opacity.FinalValue));
        }
        #endregion

        IsValidate = true;
    }
}
