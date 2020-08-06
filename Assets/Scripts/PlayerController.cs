using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xspeed = 15f;
    [Tooltip("In ms^-1")] [SerializeField] float yspeed = 15f;
    [Tooltip("in m")] [SerializeField] float xPosRange = 5f;
    [Tooltip("in m")] [SerializeField] float yPosMin = 3f;
    [Tooltip("in m")] [SerializeField] float yPosMax = 3f;
    [SerializeField] GameObject[] Guns;

    [Header("Screen Position Based")]

    [SerializeField] float yYawMax = 5f;
    [SerializeField] float xPitchMax = -5f;

    [Header("Control-Throw Based")]
    [SerializeField] float controlPitchFactor = -30f;
    [SerializeField] float zRollControlFactorMax = -30f;

    float xThrow, yThrow;
    bool isControlEnabled = true;
    // Update is called once per frame
    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }


    }



    public void OnPlayerDeath() //call by string reference ( Collision Handler)
    {
        isControlEnabled = false;
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
    void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            SetGunsActive(true);
        }
        else
        {
            SetGunsActive(false);
        }
    }

    private void SetGunsActive(bool isActive)
    {
        foreach (GameObject gun in Guns)
        {
            ParticleSystem.EmissionModule emissionModule = gun.GetComponent<ParticleSystem>().emission;
            emissionModule.enabled = isActive;
        }
    }
}
