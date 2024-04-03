using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class OnTrigger : MonoBehaviour
{
    [SerializeField]
    private UnityEvent myTrigger;

    private void OnTriggerEnter(Collider other) {
        myTrigger.Invoke();
    }
}
