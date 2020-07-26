using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //ok as long as this is the only scrip that loads scenes

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("in seconds")] [SerializeField] float levelLoadDelay = 1f;
    [Tooltip("Effects Prefab on player")][SerializeField] GameObject deathFX;

    void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        SendMessage("OnPlayerDeath");
    }

    private void ReloadScene() //string reference
    {
        SceneManager.LoadScene(1);
    }
}
