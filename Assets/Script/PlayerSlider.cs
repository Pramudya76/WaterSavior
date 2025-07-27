using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSlider : MonoBehaviour
{
    public Transform Player;
    public GameObject HandlerSlider;
    // Start is called before the first frame update
    void Start()
    {
        HandlerSlider.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position + new Vector3(0, 0.7f, 0);
    }
}
