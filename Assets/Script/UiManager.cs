using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public GameObject SettingPanel;
    private GameObject PlayerPos;
    public GameObject SettingLayer;
    // Start is called before the first frame update
    void Start()
    {
        SettingLayer.gameObject.SetActive(false);
        SettingPanel.gameObject.SetActive(false);
        PlayerPos = GameObject.FindWithTag("Player");
        Time.timeScale = 1;
    }
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SettingPanel.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void ResumeButton()
    {
        SettingPanel.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void VolumeButton()
    {
        SettingLayer.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void MainMenuButton()
    {
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            PlayerPrefs.SetFloat("CurrentPosX", PlayerPos.transform.position.x);
            PlayerPrefs.SetFloat("CurrentPosY", PlayerPos.transform.position.y);
            PlayerPrefs.SetFloat("CurrentPosZ", PlayerPos.transform.position.z);
        }
        PlayerPrefs.SetInt("SaveGame", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void MainMenuButtonBattleArea()
    {
        PlayerPrefs.SetInt("SaveGame", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitSettingLayer()
    {
        SettingLayer.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

}
