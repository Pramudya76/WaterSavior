using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySlider : MonoBehaviour
{
    public Transform enemyPos;
    private Slider sliderEnemy;
    private EnemyStatus ES;
    // Start is called before the first frame update
    void Start()
    {
        //PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        sliderEnemy = GetComponent<Slider>();
        //ES = GameObject.FindWithTag("Enemy").GetComponent<EnemyStatus>();
    }
    public void setSlider(EnemyStatus enemyStatus)
    {
        ES = enemyStatus;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = enemyPos.position + new Vector3(0, 0.5f);
        sliderEnemy.value = ES.CurrentHealth;
        Transform Handle = sliderEnemy.transform.Find("Fill Area/Fill");
        Image fill = Handle.GetComponent<Image>();
        if (sliderEnemy.value <= 0)
        {
            fill.enabled = false;
            Destroy(gameObject);
        }
        else
        {
            fill.enabled = true;
        }
    }

    

}
