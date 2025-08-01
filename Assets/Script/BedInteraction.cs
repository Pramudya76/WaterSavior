using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BedInteraction : MonoBehaviour
{
    public GameObject ButtonPrefabs;
    private GameObject Button;
    public Transform canvaPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene("DreamWorldAfterDone");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && PlayerPrefs.GetInt("WaterFinished") == 1)
        {
            Transform Player = collision.GetComponent<Transform>();
            Button = Instantiate(ButtonPrefabs, Player.transform.position + new Vector3(-0.8f, 1, 0), Quaternion.identity, canvaPos);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(Button);
        }
    }

}
