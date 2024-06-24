using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioRandom : MonoBehaviour
{
    [SerializeField]
    private AudioSource m_Source;

    // Start is called before the first frame update
    void Start()
    {
        Invoke(nameof(PlayAudio), Random.Range(15, 45)) ;
    }

    // Update is called once per frame
    void PlayAudio()
    {
        m_Source.Play(); 
        Invoke(nameof(PlayAudio), Random.Range(15, 45));
    }

    private void OnDestroy()
    {
        CancelInvoke();
    }
}
