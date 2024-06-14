using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class height : MonoBehaviour
{
    public TMP_Text height_text;

    public GameObject cage;

    private float height_cage_initial;

    // Start is called before the first frame update
    void Start()
    {
        height_cage_initial = cage.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        height_text.text = ((cage.transform.position.y - height_cage_initial) / 4).ToString("00");
    }
}
