using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private bool debug = false;
    [SerializeField] private float playerObjectiveBase = -50f;
    [SerializeField] private float fogEndDistanceBase = 15f;
    [SerializeField] private float faunaDensityBase = 1f;
    [SerializeField] private float speedSpawnFishBase = 0.5f;

    [SerializeField] private GoalElevator cage;
    [SerializeField] private FishSpawn seaFloor;
    [SerializeField] private Canvas menu;

    public float PlayerObjective { get; private set; }
    public float FogEndDistance { get; private set; }
    public float FaunaDensity { get; private set; }

    public bool IsValidate { get; private set; }

    private void Start()
    {
        PlayerObjective = playerObjectiveBase;
        FogEndDistance = fogEndDistanceBase;
        FaunaDensity = faunaDensityBase;
        if (debug) ValidDifficulty();
    }

    public void SetPlayerObjective(float coeff)
    {
        PlayerObjective = playerObjectiveBase + playerObjectiveBase * coeff;
        //DynamicText(PlayerObjective, textElt);
    }
    public void SetFogDistanceEnd(float coeff)
    {
        FogEndDistance = fogEndDistanceBase + fogEndDistanceBase * coeff;
        //DynamicText(FogEndDistance, textElt);
    }
    public void SetFaunaDensity(float coeff)
    {
        FaunaDensity = coeff;
        //DynamicText(FaunaDensity, textElt);
    }

    public void DynamicText(float value, TextMeshPro textElt = null)
    {
        if (textElt == null) return;
        textElt.text = value.ToString();
    }

    public void ValidDifficulty()
    {
        menu.enabled = false;
        #region playerObjective
        cage.HeightGoal = PlayerObjective;
        seaFloor.transform.position = new Vector3(seaFloor.transform.position.x, PlayerObjective, seaFloor.transform.position.z);
        #endregion

        #region fogDistance
        RenderSettings.fogEndDistance = FogEndDistance;
        #endregion

        #region FaunaDensity
        seaFloor.StartSpawn(speedSpawnFishBase * FaunaDensity);
        #endregion

        IsValidate = true;
    }
}
