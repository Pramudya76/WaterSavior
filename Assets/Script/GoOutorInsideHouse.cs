using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoOutorInsideHouse : MonoBehaviour
{
    public GameObject ButtonPrefabs;
    private GameObject Button;
    private bool isButtonActive = false;
    public Transform canvaPos;
    private GameObject PlayerPos;
    public String SceneTargetName;
    //public String SceneName;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPos = GameObject.FindWithTag("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonActive)
        {
            Button.transform.position = PlayerPos.transform.position + new Vector3(0.8f, 0.5f, 0);
            if (Input.GetKeyDown(KeyCode.F))
            {
                PlayerPrefs.SetFloat("PlayerX_" + SceneManager.GetActiveScene().name, PlayerPos.transform.position.x);
                PlayerPrefs.SetFloat("PlayerY_" + SceneManager.GetActiveScene().name, PlayerPos.transform.position.y);
                PlayerPrefs.SetFloat("PlayerZ_" + SceneManager.GetActiveScene().name, PlayerPos.transform.position.z);
                PlayerPrefs.Save();
                SceneManager.LoadScene(SceneTargetName);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPos = collision.gameObject;
            Button = Instantiate(ButtonPrefabs, PlayerPos.transform.position + new Vector3(0.8f, 0.5f, 0), Quaternion.identity, canvaPos);
            Button.layer = 5;
            isButtonActive = true;


        }
    }
    
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(Button);
            isButtonActive = false;
        }
    }

}
