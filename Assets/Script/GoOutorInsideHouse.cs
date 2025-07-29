using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoOutorInsideHouse : MonoBehaviour
{
    public GameObject ButtonPrefabs;
    private GameObject Button;
    private bool isButtonActive = false;
    public Transform canvaPos;
    private Transform PlayerPos;
    public String SceneTargetName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isButtonActive)
        {
            Button.transform.position = PlayerPos.position + new Vector3(0.8f, 0.5f, 0);
            if (Input.GetKeyDown(KeyCode.F))
            {
                SceneManager.LoadScene(SceneTargetName);
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerPos = collision.transform;
            Button = Instantiate(ButtonPrefabs, collision.transform.position + new Vector3(0.8f, 0.5f, 0), Quaternion.identity, canvaPos);
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
