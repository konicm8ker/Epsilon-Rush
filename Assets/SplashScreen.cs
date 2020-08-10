using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    void Start()
    {
        Invoke("LoadFirstLevel", 4f);
    }

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(1);
    }
}
