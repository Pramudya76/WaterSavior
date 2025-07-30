using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPos : MonoBehaviour
{
    public String SceneName;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        if (PlayerPrefs.HasKey("PlayerX_" + SceneName) && PlayerPrefs.HasKey("PlayerY_" + SceneName) && PlayerPrefs.HasKey("PlayerZ_" + SceneName))
        {
            float x = PlayerPrefs.GetFloat("PlayerX_" + SceneName);
            float y = PlayerPrefs.GetFloat("PlayerY_" + SceneName);
            float z = PlayerPrefs.GetFloat("PlayerZ_" + SceneName);
            Player.transform.position = new Vector3(x, y, z);
            Debug.Log("Berjalan posisi player");
        }
        else
        {
            Debug.Log("Tdk berjalan posisi player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
