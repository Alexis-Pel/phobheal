using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

public class FishSpawn : MonoBehaviour
{
    [SerializeField] private UnderwaterEffect water;
    [SerializeField] private GameObject[] fishModel;
    [SerializeField] private GoalElevator cage;

    [SerializeField] private float minDistanceSpawn;
    [SerializeField] private float maxDistanceSpawn;
    [SerializeField] private float spawnSpeed;

    [SerializeField] private int maxNbFishGroup;
    private List<GameObject> fishGroups;

    // See if the fishgroups has a time limite before disapearing -> can regenerate one at a different location
    // maybe create a different script that manage each group individualy. That way this script does not do it (like their movement)

    private void Start()
    {
        fishGroups = new List<GameObject>();
        InvokeRepeating(nameof(SpawnFishes), 0f, spawnSpeed);
    }

    private void SpawnFishes()
    {
        if (!water.IsUnderWater() || fishGroups.Count >= maxNbFishGroup) return;

        GameObject fish = fishModel[Random.Range(0, fishModel.Length - 1)]; // Generate a random gameObject from the list
        GameObject fishGroup = Instantiate(fish, GenerateCoord(), Quaternion.Euler(new Vector3(0f, Random.Range(0f, 360f), 0f)), transform);

        fishGroups.Add(fishGroup);
    }

    //Generate a random distance from the cage then generate a fish on a random point on the sphere the distance describe
    private Vector3 GenerateCoord()
    {
        float distance2D = Random.Range(minDistanceSpawn, maxDistanceSpawn);
        Vector2 coord = RandomExtends.OnUnitCircle() * distance2D;
        Vector3 genCoord = new (coord.x + cage.transform.position.x, Random.Range(cage.HeightGoal, water.WaterNivelY), coord.y + cage.transform.position.z);
        return genCoord;
    }
}
