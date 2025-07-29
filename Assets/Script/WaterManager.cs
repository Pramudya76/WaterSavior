using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterManager : MonoBehaviour
{
    //private PurifyWater PW;
    public String WaterName;
    // Start is called before the first frame update
    void Start()
    {
        //PW = GameObject.FindWithTag("DirtWater").GetComponent<PurifyWater>();
        WaterName = PlayerPrefs.GetString("CurrentWater");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
