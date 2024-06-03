using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField] private float playerObjective = -50f;
    [SerializeField] private float fogDistanceStart = 25f;
    [SerializeField] private float faunaDensity = 1f;
    [SerializeField] private float speedSpawnFish = 0.5f;

    [SerializeField] private GoalElevator cage;
    [SerializeField] private FishSpawn seaFloor;

    public void SetPlayerObjective(float playerObjective) => this.playerObjective = playerObjective;
    public void SetFogDistanceStart(float fogDistanceStart) => this.fogDistanceStart = fogDistanceStart;
    public void SetFaunaDensity(float faunaDensity) => this.faunaDensity = faunaDensity;

    public void ValidDifficulty()
    {
        #region playerObjective
        cage.HeightGoal = playerObjective;
        seaFloor.transform.position = new Vector3(seaFloor.transform.position.x, playerObjective, seaFloor.transform.position.z);
        #endregion

        #region fogDistance
        RenderSettings.fogStartDistance = fogDistanceStart;
        #endregion

        #region FaunaDensity
        seaFloor.StartSpawn(speedSpawnFish * faunaDensity);
        #endregion
    }
}
