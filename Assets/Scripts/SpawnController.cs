using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField]
    private GameObject AI;
    [SerializeField]
    private int xPosition;
    [SerializeField]
    private int zPosition;
    [SerializeField]
    private int total;

    public int spawnedCount;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        Debug.Log("ENUMY SPAWNER INIT");
        while (spawnedCount < total)
        {
            Debug.Log("ENEMY SPAWNED");
            xPosition = Random.Range(2, 5);
            zPosition = Random.Range(4, 0);
            Instantiate(AI, new Vector3(xPosition, 0, zPosition), Quaternion.identity);
            yield return new WaitForSeconds(1);
            spawnedCount++;
            Debug.Log("count eney : " + spawnedCount);
        }
    }
}
