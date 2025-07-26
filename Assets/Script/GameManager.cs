using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider healthPlayerSlider;
    public GameObject HealthSliderPlayer;
    public String turn = "Player";
    private PlayerAttack PA;
    public Image fillSliderPlayer;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        HealthSliderPlayer.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (PA.health < 200)
        {
            HealthSliderPlayer.gameObject.SetActive(true);
        }
        healthPlayerValue();
    }

    public void healthPlayerValue()
    {
        healthPlayerSlider.value = PA.health;
        if (PA.health <= 0)
        {
            fillSliderPlayer.enabled = false;
        }
        else
        {
            fillSliderPlayer.enabled = true;
        }
    }


}
