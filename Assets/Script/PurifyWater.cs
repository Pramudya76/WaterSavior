using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PurifyWater : MonoBehaviour
{
    public GameObject ButtonPrefabs;
    private GameObject Button;
    public Transform canvaPos;
    private Transform PlayerPos;
    private bool isButtonActive = false;
    public int jumlahEnemy;
    public GameObject[] dirtWater;
    public String nameWater;
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt(nameWater) == 1)
        {
            for (int a = 0; a < dirtWater.Length; a++)
            {
                Destroy(dirtWater[a]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonActive)
        {
            Button.transform.position = PlayerPos.position + new Vector3(0.8f, 0.5f, 0);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerPrefs.SetString("CurrentWater", nameWater);
                PlayerPrefs.Save();
                PlayerPrefs.SetInt("JumlahEnemy", jumlahEnemy);
                SceneManager.LoadScene("BattleArea");
            }
        }
        Debug.Log(PlayerPrefs.GetInt(nameWater));
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerPrefs.GetInt(nameWater) == 0)
        {
            PlayerPos = collision.transform;
            Button = Instantiate(ButtonPrefabs, collision.transform.position + new Vector3(0.8f, 0.5f, 0), Quaternion.identity, canvaPos);
            //Button.gameObject.SetActive(true);
            isButtonActive = true;
            
            
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" &&  PlayerPrefs.GetInt(nameWater) == 0)
        {
            Destroy(Button);
            //Button.gameObject.SetActive(false);
            isButtonActive = false;
        }
    }

}
