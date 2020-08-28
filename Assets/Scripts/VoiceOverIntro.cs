using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoiceOverIntro : MonoBehaviour
{
    AudioSource audioSource;
    public GameObject holoAvatar;
    

    void Awake()
    {
        PreserveIntroObjects(this.gameObject);
        PreserveIntroObjects(holoAvatar);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Invoke("PlayIntroVO", 1.5f);
    }

    void Update()
    {
        if(Time.time > 6f)
        {
            DestroyIntroObjects(this.gameObject);
            DestroyIntroObjects(holoAvatar);
        }
    }

    private void PlayIntroVO()
    {
        audioSource.Play();
        holoAvatar.SetActive(true);
    }

    private void PreserveIntroObjects(GameObject value)
    {
        DontDestroyOnLoad(value);
    }

    private void DestroyIntroObjects(GameObject value)
    {
        Destroy(value);
    }
}
