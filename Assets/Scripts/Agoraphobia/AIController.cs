
using System;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;

public class AIController : MonoBehaviour
{
    private GameObject Destination;
    private NavMeshAgent Agent;
    
    //[SerializeField]
    private string DestinationTagName;

    private const string DestinationTagNameStart = "Destination_00";
    // Has we have 2 destinations for now, we need to generate a number between 1 and 2
    // Thus we set the constant to 2, this way when we call Random Next() method
    // We give range from 1 to 2, to generate int between 1 and 2 ( 1 or 2 )
    private const int DESTINATION_SIZE = 7;

    private string _lastDestination;
    
    // Start is called before the first frame update
    void Start()
    {
        DestinationTagName = DestinationTagNameStart + GenerateRandomDestinationIndex();
        Debug.Log("Destination target point generated : " + DestinationTagName);
        Destination = GameObject.FindGameObjectWithTag(DestinationTagName);
        _lastDestination = DestinationTagName;
        DestinationTagName = "";
        Agent = GetComponent<NavMeshAgent>();

        Agent.SetDestination(Destination.transform.position);
        
        Debug.Log("Destination type ");
        Debug.Log(Agent.transform.position);

    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals(_lastDestination))
        {
            Debug.Log("GAME OBJECT TOUCHED : ");
            
            Debug.Log(Destination.name);
            SetNextDestination();
        }
    }

    private void SetNextDestination()
    {
        Destination = GenerateDestination();
        Agent.SetDestination(Destination.transform.position);
        
        _lastDestination = Destination.name;
        
        Debug.Log("DESTINATION NEXT GENERATION NAME : " + Destination.name);
    }

    private int GenerateRandomDestinationIndex()
    {
        Random rand = new Random();
        return rand.Next(1, DESTINATION_SIZE);
    }

    private GameObject GenerateDestination()
    {
        Debug.Log("Generate new destination");
        int destinationIndex = GenerateRandomDestinationIndex();

        Debug.Log("previous destination : " + _lastDestination);
        Debug.Log("New destination : " + destinationIndex);
        string destinationTag = DestinationTagNameStart + destinationIndex.ToString();
        
        Debug.Log("Destination tag generated here : " + destinationTag);
        if (destinationTag.Equals(_lastDestination))
        {
            Debug.Log("Destination equals to previous destination");
            if (destinationIndex == DESTINATION_SIZE)
            {
                destinationIndex = destinationIndex - 1;
            }
            else
            {
                destinationIndex = destinationIndex + 1;
            }

            destinationTag = DestinationTagNameStart + destinationIndex;
        }

        return GameObject.FindGameObjectWithTag(destinationTag);
    }

}
