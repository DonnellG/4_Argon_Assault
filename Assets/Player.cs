using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{
    [Tooltip("In ms^-1")] [SerializeField] float xspeed = 15f;
    [Tooltip("in m")][SerializeField] float xPosRange = 5f;

    [Tooltip("In ms^-1")] [SerializeField] float yspeed = 15f;
    [Tooltip("in m")] [SerializeField] float yPosMin = 3f;
    [Tooltip("in m")] [SerializeField] float yPosMax = 3f;

    [SerializeField] float xPitchMax = -5f;
    [SerializeField] float controlPitchFactor = -30f;

    [SerializeField] float yYawMax = 5f;


    [SerializeField] float zRollControlFactorMax = -30f;

    float xThrow, yThrow;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();

    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * xPitchMax;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;
        float pitch = pitchDueToPosition + pitchDueToControlThrow;

        float yaw = transform.localPosition.x * yYawMax;

        float roll = xThrow * zRollControlFactorMax;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        float xOffset = xThrow * xspeed * Time.deltaTime;

        float rawXPos = transform.localPosition.x + xOffset;
        float clampedXPos = Mathf.Clamp(rawXPos, -xPosRange, xPosRange);

        yThrow = CrossPlatformInputManager.GetAxis("Vertical");
        float yOffset = yThrow * yspeed * Time.deltaTime;

        float rawYPos = transform.localPosition.y + yOffset;
        float clampedYPos = Mathf.Clamp(rawYPos, -yPosMin, yPosMax);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }
}
