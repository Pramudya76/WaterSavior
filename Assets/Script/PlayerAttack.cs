using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttack : MonoBehaviour
{
    [HideInInspector] public float health = 200f;
    private Vector2 originalPos;
    private float moveSpeed = 9f;
    private GameManager GM;
    public Transform PosEnemy;
    // Start is called before the first frame update
    void Start()
    {
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        //enemy = GameObject.FindWithTag("Enemy");
        originalPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && GM.turn == "Player")
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                GameObject enemyTarget = hit.collider.gameObject;
                PosEnemy = hit.collider.transform;
                EnemyStatus enemyStatus = enemyTarget.GetComponent<EnemyStatus>();


                StartCoroutine(MoveToEnemyandBack(PosEnemy.position, enemyStatus));
                
                Debug.Log(hit.collider.name + " GameObject");
            }

        }
        Debug.Log(health);
    }

    IEnumerator MoveToPos(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) >= 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
        //yield return StartCoroutine(MoveToPos(originalPos));
        //isMoving = false;
    }

    IEnumerator MoveToEnemyandBack(Vector2 enemyPos, EnemyStatus enemyStatus)
    {
        yield return StartCoroutine(MoveToPos(enemyPos));
        GM.SpawnEnemySlider(enemyPos + new Vector2(0, 0.5f), PosEnemy);
        enemyStatus.CurrentHealth -= 25f;
        Debug.Log(enemyStatus.CurrentHealth + " darah musuh");
        yield return new WaitForSeconds(1f);

        yield return StartCoroutine(MoveToPos(originalPos));
        GM.turn = "Enemy";
    }




}
