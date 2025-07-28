using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [HideInInspector] public float health = 50f;
    public GameObject[] enemy;
    private GameObject Player;
    private float moveSpeed = 9f;
    private GameManager GM;
    private bool isMoving = false;
    private PlayerAttack PA;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        GM.SpawnEnemy();
        enemy = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void Update()
    {


        if (GM.turn == "Enemy" && !isMoving)
        {
            isMoving = true;
            StartCoroutine(MoveAndBack());
            
        }

    }

    IEnumerator MoveToPos(Vector2 pos, GameObject objt)
    {

        while (Vector2.Distance(objt.transform.position, pos) >= 0.3f)
        {
            objt.transform.position = Vector2.MoveTowards(objt.transform.position, pos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        objt.transform.position = pos;

    }

    IEnumerator MoveAndBack()
    {
        List<GameObject> aliveEnemies = new List<GameObject>();
        foreach (GameObject e in enemy)
        {
            if (e != null)
            {
                aliveEnemies.Add(e);
            }
        }
        int angkaRandom = Random.Range(0, aliveEnemies.Count);
        GameObject enemyChose = aliveEnemies[angkaRandom];
        Vector2 enemyPos = enemyChose.transform.position;

        Vector2 playerPos = (Vector2)Player.transform.position + new Vector2(1, 0);

        Vector2 enemyBack = (Vector2)enemyPos + new Vector2(0.3f, 0);
        yield return StartCoroutine(MoveToPos(playerPos, enemyChose));
        PA.health -= 10f;
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(MoveToPos(enemyBack, enemyChose));
        GM.turn = "Player";
        isMoving = false;
    }

}
