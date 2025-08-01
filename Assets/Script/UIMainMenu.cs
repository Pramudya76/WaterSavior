using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    public GameObject PanelBefore;
    public GameObject PanelAfter;
    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("SaveData", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("SleepScene");
    }

    public void NewGame()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        SceneManager.LoadScene("SleepScene");
    }


}
