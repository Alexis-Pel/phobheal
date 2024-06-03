using System.Collections.Generic;
using UnityEngine;
using Utils;

public class FishSpawn : MonoBehaviour
{
    [SerializeField] private UnderwaterEffect water;
    [SerializeField] private GameObject[] fishModel;
    [SerializeField] private GoalElevator cage;

    [SerializeField] private float minDistanceSpawn;
    [SerializeField] private float maxDistanceSpawn;

    // See if the fishgroups has a time limite before disapearing -> can regenerate one at a different location
    // maybe create a different script that manage each group individualy. That way this script does not do it (like their movement)

    public void StartSpawn(float spawnSpeed)
    {
        InvokeRepeating(nameof(SpawnFishes), 0f, spawnSpeed);
    }

    private void SpawnFishes()
    {
        if (!water.IsUnderWater()) return;

        GameObject fish = fishModel[Random.Range(0, fishModel.Length)]; // Generate a random gameObject from the list
        Instantiate(fish, GenerateCoord(), Quaternion.Euler(new Vector3(fish.transform.rotation.eulerAngles.x, Random.Range(0f, 360f), fish.transform.rotation.eulerAngles.z)), transform);
    }

    //Generate a random distance from the cage then generate a fish on a random point on the sphere the distance describe
    private Vector3 GenerateCoord()
    {
        float distance2D = Random.Range(minDistanceSpawn, maxDistanceSpawn);
        Vector2 coord = RandomExtends.OnUnitCircle() * distance2D;
        float height = Random.Range(cage.HeightGoal, water.WaterNivelY);
        Vector3 genCoord = new (coord.x + cage.transform.position.x, height, coord.y + cage.transform.position.z);
        return genCoord;
    }
}
