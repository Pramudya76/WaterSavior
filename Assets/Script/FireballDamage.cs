using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballDamage : MonoBehaviour
{
    private PlayerAttack PA;
    private TurnManager TM;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        TM = GameObject.FindWithTag("TurnManager").GetComponent<TurnManager>();
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyStatus ES = collision.GetComponent<EnemyStatus>();
            ES.CurrentHealth -= 15f;
            Debug.Log("Health Enemy " + collision.name + " : " + ES.CurrentHealth);
            if (ES.CurrentHealth <= 0)
            {
                Destroy(collision);
                TM.displayQueue.RemoveAll(unit => unit == collision);
            }
            Destroy(gameObject);
        }
    }

}
