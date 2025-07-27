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
    public GameObject SliderEnemyPrefabs;
    public Transform ParentCanva;
    private EnemyStatus ES;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        HealthSliderPlayer.gameObject.SetActive(false);
        ES = GameObject.FindWithTag("Enemy").GetComponent<EnemyStatus>();
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

    public void SpawnEnemySlider(Vector3 enemy, Transform TargetPos)
    {
        GameObject sliderEnemy = Instantiate(SliderEnemyPrefabs, enemy, Quaternion.identity, ParentCanva);
        Transform handler = sliderEnemy.transform.Find("Handle Slide Area");
        EnemySlider enemySlider = sliderEnemy.AddComponent<EnemySlider>();

        Slider EnemySlider = sliderEnemy.GetComponent<Slider>();
        //EnemySlider.onValueChanged.AddListener(ES.EnemyHealth);
        //EnemySlider.value = ES.CurrentHealth;
        EnemySlider.maxValue = ES.CurrentHealth;

        enemySlider.enemyPos = TargetPos;
        handler.gameObject.SetActive(false);

    }


}
