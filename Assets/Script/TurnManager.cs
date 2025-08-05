using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{
    public List<MonoBehaviour> allUnits;
    public Queue<MonoBehaviour> turnQueue = new Queue<MonoBehaviour>();
    public List<MonoBehaviour> displayQueue = new List<MonoBehaviour>();
    private bool isNow = false;
    private bool turnProgress = false;
    private GameManager GM;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        PlayerAttack Player = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        if (Player != null)
        {
            allUnits.Add(Player);
        }

        GameObject[] enemyObjects = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemyObjt in enemyObjects)
        {
            EnemyStatus Enemy = enemyObjt.GetComponent<EnemyStatus>();
            if (Enemy != null)
            {
                allUnits.Add(Enemy);
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        FillGauses();
        if (turnQueue.Count > 0 && !isNow)
        {
            StartCoroutine(HandleTurn());
        }
        
    }

    public void FillGauses()
    {
        foreach (var unit in allUnits)
        {
            if (unit == null) continue;
            if (unit is PlayerAttack Player)
            {
                Player.gauge += Player.speed * Time.deltaTime;
                while (Player.gauge >= Player.gaugetoAct)
                {
                    Player.gauge -= Player.gaugetoAct;
                    turnQueue.Enqueue(Player);
                    displayQueue.Add(Player);
                }
            }
            else if (unit is EnemyStatus Enemy)
            {
                Enemy.gauge += Enemy.speed * Time.deltaTime;
                while (Enemy.gauge >= Enemy.gaugetoAct)
                {
                    Enemy.gauge -= Enemy.gaugetoAct;
                    turnQueue.Enqueue(Enemy);
                    displayQueue.Add(Enemy);
                }
            }
        }
    }

    IEnumerator HandleTurn()
    {
        isNow = true;
        turnProgress = true;
        while (turnQueue.Count > 0)
        {
            var unit = turnQueue.Dequeue();
            if (unit is PlayerAttack Player)
            {
                GM.turn = "Player";
                Player.isPlayerTurn = true;
                yield return new WaitUntil(() => Player.isPlayerTurn == false);
            }
            else if (unit is EnemyStatus Enemy)
            {
                if (Enemy == null) continue;
                EnemyAttack EA = Enemy.GetComponent<EnemyAttack>();
                if (EA != null)
                {
                    GM.turn = "Enemy";
                    if (GM.turn == "Enemy")
                    {
                        yield return StartCoroutine(EA.MoveAndBack());
                    }
                }
            }
            displayQueue.Remove(unit);
        }
        isNow = false;
        turnProgress = false;
    }
}
