using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {

        
            SetUpSingleton();
        
    }

    private void SetUpSingleton()
    {

        if (FindObjectsOfType(GetType()).Length > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {

                DontDestroyOnLoad(gameObject);
            
        }
        
    }
    private void DestroyEveryoneElse()
    {
        var musicPlayers = FindObjectsOfType<MusicPlayer>();
        foreach (MusicPlayer item in musicPlayers)
        {
            if (FindObjectOfType<MusicPlayer>() != item)
                Destroy(item);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
    private void Start()
    {
       
    }
}
