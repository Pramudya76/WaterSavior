using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyData data;
    public String unitName;
    public int speed;
    public float gauge = 0f;
    public float gaugetoAct = 100f;
    [HideInInspector] public float CurrentHealth;
    private SpriteRenderer SpriteRenderer;
    private float dissolveAmount = 0;
    private TurnManager TM;
    // Start is called before the first frame update
    void Start()
    {
        TM = GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        if (data != null)
        {
            CurrentHealth = data.maxHealth;
            unitName = data.enemyName;
            speed = data.speed;
            gaugetoAct = 100f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (CurrentHealth <= 0)
        {
            StartCoroutine(CDBeforeDie());
        }
    }

    public void EnemyHealth(float value)
    {
        CurrentHealth = value;
    }

    IEnumerator CDBeforeDie()
    {
        dissolveAmount += Time.deltaTime;
        dissolveAmount = Mathf.Clamp(dissolveAmount, 0, 1.1f);
        SpriteRenderer.material.SetFloat("_DissolveAmount", dissolveAmount);
        yield return new WaitForSeconds(1f);
        TM.allUnits.Remove(gameObject.GetComponent<EnemyStatus>());
        TM.displayQueue.RemoveAll(unit => unit == this);
        GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TurnImage();
        Destroy(gameObject);
    }

}
