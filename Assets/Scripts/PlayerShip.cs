using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("In m/s^-1")][SerializeField] float speed = 18f;
    [Tooltip("In m")][SerializeField] float xRange = 14.5f;
    [Tooltip("In m")][SerializeField] float yRange = 8f;
    [SerializeField] float posPitchFactor = -3f;
    [SerializeField] float posYawFactor = 3f;
    [SerializeField] float controlPitchFactor = -20f;
    [SerializeField] float controlRollFactor = -30f;

    float xThrow, yThrow;

    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {

        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        Debug.Log("X Throw: " + xThrow + " | Y Throw: " + yThrow );

        float xOffset = xThrow * speed * Time.deltaTime; // Gets current x offset per frame
        float yOffset = yThrow * speed * Time.deltaTime; // Gets current y offset per frame
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
}
