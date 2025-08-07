using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject PanelBefore;
    public GameObject PanelAfter;
    public GameObject SettingLayer;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        SettingLayer.gameObject.SetActive(false);
        if (PlayerPrefs.HasKey("SaveData"))
        {
            PanelAfter.gameObject.SetActive(true);
            PanelBefore.gameObject.SetActive(false);
        }
        else
        {
            PanelAfter.gameObject.SetActive(false);
            PanelBefore.gameObject.SetActive(true);
        }
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("SaveData", 1);
        PlayerPrefs.Save();
        StartCoroutine(CDFadePanel(1f, "SleepScene"));
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("SaveData", 1);
        PlayerPrefs.Save();
        StartCoroutine(CDFadePanel(1f, "SleepScene"));
    }

    public void LoadGame()
    {
        StartCoroutine(CDFadePanel(1f, PlayerPrefs.GetString("CurrentScene")));
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void VolumeButton()
    {
        SettingLayer.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ExitSettingLayer()
    {
        SettingLayer.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator CDFadePanel(float duration, String nameScene)
    {
        animator.SetTrigger("FinishScene");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(nameScene);
    }

}
