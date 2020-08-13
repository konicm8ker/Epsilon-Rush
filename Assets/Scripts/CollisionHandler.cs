using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("FX prefab on player")][SerializeField] GameObject deathFX;
    [Tooltip("In seconds")][SerializeField] float levelDelay = 1f;

    void OnTriggerEnter(Collider collider)
    {
        StartDeathSequence();
        deathFX.SetActive(true);
        Invoke("ReloadLevel", levelDelay);
    }

    private void StartDeathSequence()
    {
        gameObject.SendMessage("OnPlayerDeath"); // Disable controls on PlayerController
    }

    private void ReloadLevel() // String referenced
    {
        SceneManager.LoadScene(1);
    }

}
