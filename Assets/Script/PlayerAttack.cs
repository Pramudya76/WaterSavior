using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Vector2 originalPos;
    private float moveSpeed = 5f;
    private GameManager GM;
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
                StartCoroutine(MoveToEnemyandBack(hit.collider.transform.position));

                Debug.Log(hit.collider.name + " GameObject");
            }
            
        }
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

    IEnumerator MoveToEnemyandBack(Vector2 enemyPos)
    {
        yield return StartCoroutine(MoveToPos(enemyPos));

        yield return new WaitForSeconds(2f);

        yield return StartCoroutine(MoveToPos(originalPos));
        GM.turn = "Enemy";
    }




}
