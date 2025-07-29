using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public GameObject gameOverPanel;
    public GameObject[] enemy;
    public Transform[] enemySpawnPos;
    private WaterManager WM;
    public GameObject WinGamePanel;
    //private PurifyWater PW;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        HealthSliderPlayer.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        WinGamePanel.gameObject.SetActive(false);
        WM = GameObject.FindWithTag("WaterManager").GetComponent<WaterManager>();
        //PW = GameObject.FindWithTag("DirtWater").GetComponent<PurifyWater>();
        //enemySpawnPos = new Vector3()
        Time.timeScale = 1;
        //ES = GameObject.FindWithTag("Enemy").GetComponent<EnemyStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PA.health < 200)
        {
            HealthSliderPlayer.gameObject.SetActive(true);
        }
        healthPlayerValue();
        GameObject enemyObjt = GameObject.FindWithTag("Enemy");
        if (enemyObjt == null)
        {
            StartCoroutine(WinGame());
        }

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
        ES = TargetPos.GetComponent<EnemyStatus>();
        EnemySlider enemySlider = sliderEnemy.AddComponent<EnemySlider>();



        Slider EnemySlider = sliderEnemy.GetComponent<Slider>();
        enemySlider.setSlider(ES);
        //EnemySlider.onValueChanged.AddListener(ES.EnemyHealth);
        //EnemySlider.value = ES.CurrentHealth;
        EnemySlider.maxValue = ES.CurrentHealth;

        enemySlider.enemyPos = TargetPos;
        handler.gameObject.SetActive(false);

    }

    public void SpawnEnemy(int jumlahEnemy)
    {
        List<Transform> enemySpawn = new List<Transform>(enemySpawnPos);
        // foreach (Transform pos in enemySpawnPos)
        // {
        //     if (pos == null) continue;
        //     int enemySpawnRandom = UnityEngine.Random.Range(0, enemy.Length);
        //     int enemySpawnLocation = UnityEngine.Random.Range(0, enemySpawn.Count);
        //     GameObject enemyPrefabs = Instantiate(enemy[enemySpawnRandom], pos.position, Quaternion.identity);

        // }

        for (int a = 0; a < jumlahEnemy; a++)
        {
            int enemySpawnRandom = UnityEngine.Random.Range(0, enemy.Length);
            int enemySpawnLocation = UnityEngine.Random.Range(0, enemySpawn.Count);
            GameObject enemyPrefabs = Instantiate(enemy[enemySpawnRandom], enemySpawn[a].position, Quaternion.identity);
        }

        //enemySpawn.RemoveAt(enemySpawnLocation);
    }

    public void gameOver()
    {
        gameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void BackToPlayAgain()
    {
        SceneManager.LoadScene("BattleArea");
    }

    IEnumerator WinGame()
    {
        WinGamePanel.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt(WM.WaterName, 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Outdoor");
    }


}
