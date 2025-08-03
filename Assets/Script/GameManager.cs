using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
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
    private int indexWaterDone;
    private AudioManager AM;
    public GameObject WeaponPlayer;
    private Quaternion OriginalRotation;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        HealthSliderPlayer.gameObject.SetActive(false);
        gameOverPanel.gameObject.SetActive(false);
        WinGamePanel.gameObject.SetActive(false);
        WM = GameObject.FindWithTag("WaterManager").GetComponent<WaterManager>();
        AM = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        OriginalRotation = WeaponPlayer.transform.rotation;
        Time.timeScale = 1;
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
        EnemySlider.maxValue = ES.CurrentHealth;

        enemySlider.enemyPos = TargetPos;
        handler.gameObject.SetActive(false);
    }

    public void SpawnEnemy(int jumlahEnemy)
    {
        List<Transform> enemySpawn = new List<Transform>(enemySpawnPos);
        for (int a = 0; a < jumlahEnemy; a++)
        {
            int enemySpawnRandom = UnityEngine.Random.Range(0, enemy.Length);
            int enemySpawnLocation = UnityEngine.Random.Range(0, enemySpawn.Count);
            GameObject enemyPrefabs = Instantiate(enemy[enemySpawnRandom], enemySpawn[a].position, Quaternion.identity);
        }
    }

    public void gameOver()
    {
        AM.GameOverSound.Play();
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
        AM.SuccesSound.Play();
        yield return new WaitForSeconds(1.5f);
        PlayerPrefs.SetInt(WM.WaterName, 1);
        indexWaterDone = PlayerPrefs.GetInt("WaterDone", 0);
        indexWaterDone += 1;
        PlayerPrefs.SetInt("WaterDone", indexWaterDone);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Outdoor");
    }

    public void CallRotationWeapon()
    {
        StartCoroutine(RotateWeaponPlayer());
    }

    IEnumerator RotateWeaponPlayer()
    {
        while (Quaternion.Angle(WeaponPlayer.transform.rotation, Quaternion.Euler(0, 0, -20)) > 0.1f)
        {
            WeaponPlayer.transform.rotation = Quaternion.RotateTowards(WeaponPlayer.transform.rotation, Quaternion.Euler(0, 0, -20), 5f * Time.deltaTime);
            
        }
        yield return new WaitForSeconds(0.5f);
        while (Quaternion.Angle(WeaponPlayer.transform.rotation, OriginalRotation) > 0.1f)
        {
            WeaponPlayer.transform.rotation = Quaternion.RotateTowards(WeaponPlayer.transform.rotation, OriginalRotation, 5f * Time.deltaTime);
        }
    }


}
