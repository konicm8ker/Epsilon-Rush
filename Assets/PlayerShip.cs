using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerShip : MonoBehaviour
{
    [Tooltip("In m/s^-1")][SerializeField] float xSpeed = 4f;
    [Tooltip("In m/s^-1")][SerializeField] float ySpeed = 4f;
    [Tooltip("In m")][SerializeField] float xRange = 10f;
    [Tooltip("In m")][SerializeField] float yRange = 5f;


    void Update()
    {
        float xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xSpeed * Time.deltaTime; // Gets current x offset per frame
        float rawXPos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange); // Prevents going offscreen on x axis

        float yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * ySpeed * Time.deltaTime; // Gets current y offset per frame
        float rawYPos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange); // Prevents going offscreen on y axis


        transform.localPosition = new Vector3(rawXPos, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = new Vector3(transform.localPosition.x, rawYPos, transform.localPosition.z);
    }
}
