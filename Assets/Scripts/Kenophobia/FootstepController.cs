using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // Array des clips audio de bruits de pas
    private AudioSource audioSource;
    private CharacterController characterController;
    private bool isPlayingFootsteps;

    // Temps de délai entre chaque son
    public float footstepDelay = 0.5f;

    // Volume des bruits de pas
    [Range(0f, 1f)]
    public float footstepVolume = 0.5f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        characterController = GetComponent<CharacterController>();
        isPlayingFootsteps = false;
    }

    void Update()
    {
        if (characterController.isGrounded && characterController.velocity.magnitude > 2f && !isPlayingFootsteps)
        {
            StartCoroutine(PlayFootstepSound());
        }
    }

    IEnumerator PlayFootstepSound()
    {
        isPlayingFootsteps = true;
        AudioClip clip = GetRandomFootstepSound();
        audioSource.PlayOneShot(clip, footstepVolume);
        yield return new WaitForSeconds(footstepDelay);
        isPlayingFootsteps = false;
    }

    AudioClip GetRandomFootstepSound()
    {
        return footstepSounds[Random.Range(0, footstepSounds.Length)];
    }
}

