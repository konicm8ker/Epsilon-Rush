using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{

    [Header("General")]
    [Tooltip("In m/s^-1")][SerializeField] float controlSpeed = 18f;
    [Tooltip("In m")][SerializeField] float xRange = 14.5f;
    [Tooltip("In m")][SerializeField] float yRange = 8f;
    [SerializeField] GameObject[] guns = null;

    [Header("Screen-position Based")]
    [SerializeField] float posPitchFactor = -3f;
    [SerializeField] float posYawFactor = 3f;

    [Header("Control-throw Based")]
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;

    [Header("Sound Effects")]
    [SerializeField] AudioClip laserBlast = null;
    AudioSource audioSource;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    bool playFiringSFX = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        StartCoroutine(AdjustFiringTime(0.1f));
    }

    void Update()
    {
        if(isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    IEnumerator AdjustFiringTime(float waitTime)
    {
        while(true)
        {
            if(playFiringSFX)
            {
                audioSource.PlayOneShot(laserBlast, 0.15f);
            }
            yield return new WaitForSeconds(waitTime);
        }
    } 

    void OnPlayerDeath() // String referenced
    {
        isControlEnabled = false;
        FiringTrigger(false);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        // Debug.Log("X Throw: " + xThrow + " | Y Throw: " + yThrow );

        float xOffset = xThrow * controlSpeed * Time.deltaTime; // Gets current x offset per frame
        float yOffset = yThrow * controlSpeed * Time.deltaTime; // Gets current y offset per frame
        float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange); // Prevents going offscreen on x axis
        float rawYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange); // Prevents going offscreen on y axis

        transform.localPosition = new Vector3(rawXPos, rawYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        // Rotates player ship based on pos and throw on y axis
        float pitchDueToPos = transform.localPosition.y * posPitchFactor;
        float pitchDueToThrow = yThrow * controlPitchFactor;
        float pitch =  pitchDueToPos + pitchDueToThrow;

        // Rotates player ship based on pos on x axis
        float yawDueToPos = transform.localPosition.x * posYawFactor;
        float yaw = yawDueToPos;

        // Rotates player ship based on throw on x axis
        float rollDueToThrow = xThrow * controlRollFactor;
        float roll = rollDueToThrow;

        transform.localRotation = Quaternion.Euler(pitch,yaw,roll);
    }

    private void ProcessFiring()
    {
        // Only fire when button pressed
        if(CrossPlatformInputManager.GetButton("Fire1"))
        {
            FiringTrigger(true);
        }
        else
        {
            FiringTrigger(false);
        }
    }

    private void FiringTrigger(bool value)
    {
        foreach(GameObject gun in guns)
        {
            // Prevents bullets that have been fired from suddenly disappearing
            var emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = value;
        }
        playFiringSFX = value;
    }

}
