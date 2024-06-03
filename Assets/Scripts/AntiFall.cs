using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiFall : MonoBehaviour
{
    private void Start()
    {
        Physics.gravity = new Vector3(0, -0.5f, 0);
    }
}
