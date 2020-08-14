using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{

    void Awake()
    {
        int numOfAudioPlayers = FindObjectsOfType<AudioPlayer>().Length;
        if(numOfAudioPlayers > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
    
}
