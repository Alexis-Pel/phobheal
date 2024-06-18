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

    public bool IsValidate { get; private set; }

    private void Start()
    {
        if (debug) ValidDifficulty();
        cage.AllowedMoving(false);
    }

    public void ValidDifficulty()
    {
        #region playerObjective
        cage.HeightGoal = depth.FinalValue;
        seaFloor.transform.position = new Vector3(seaFloor.transform.position.x, depth.FinalValue, seaFloor.transform.position.z);
        #endregion

        #region fogDistance
        RenderSettings.fogEndDistance = fog.FinalValue;
        #endregion

        #region FaunaDensity
        seaFloor.StartSpawn(faunaDensity.FinalValue);
        #endregion

        IsValidate = true;
        cage.AllowedMoving(true);
    }
}
