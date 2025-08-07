using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerAttack : MonoBehaviour
{
    public float health = 200f;
    private Vector2 originalPos;
    private float moveSpeed = 9f;
    private GameManager GM;
    [HideInInspector] public Transform PosEnemy;
    private SpriteRenderer spriteRenderer;
    private float dissolveAmount = 0;
    private AudioManager AM;
    Animator animator;
    public bool isPlayerTurn = false;
    public GameObject DamageUI;
    public Transform canvasPos;

    public String unitName = "Player";
    public int speed = 100;
    public float gauge = 0f;
    public float gaugetoAct = 100f;
    public bool isInQueue = false;
    public Sprite PlayerSprite;
    public bool isTurn = false;
    public Texture2D DefaultCursor;
    public Texture2D ClickCursor;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        AM = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        originalPos = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            Cursor.SetCursor(DefaultCursor, Vector2.zero, CursorMode.Auto);

            if (hit.collider != null && hit.collider.CompareTag("Enemy"))
            {
                Cursor.SetCursor(ClickCursor, Vector2.zero, CursorMode.Auto);
                if (Input.GetMouseButtonDown(0) && isPlayerTurn && !isTurn)
                {
                GameObject enemyTarget = hit.collider.gameObject;
                PosEnemy = hit.collider.transform;
                EnemyStatus enemyStatus = enemyTarget.GetComponent<EnemyStatus>();
                StartCoroutine(MoveToEnemyandBack(PosEnemy.position, enemyStatus));
                
            }

        }
        if (health <= 0)
        {
            StartCoroutine(CDBeforeDie());
        }

    }

    IEnumerator MoveToPos(Vector2 target)
    {
        while (Vector2.Distance(transform.position, target) >= 1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator MoveToEnemyandBack(Vector2 enemyPos, EnemyStatus enemyStatus)
    {
        isPlayerTurn = true;
        isTurn = true;
        animator.SetFloat("xvelocity", 1);
        animator.SetFloat("yvelocity", 1);
        yield return StartCoroutine(MoveToPos(enemyPos));
        GM.SpawnEnemySlider(enemyPos + new Vector2(0, 0.5f), PosEnemy);
        GameObject Damage = Instantiate(DamageUI, enemyPos + new Vector2(0.8f, -0.3f), Quaternion.identity, canvasPos);
        TextMeshProUGUI DamageText = Damage.GetComponent<TextMeshProUGUI>();
        DamageText.text = "25";
        enemyStatus.CurrentHealth -= 25f;
        AM.PlayerAttack.Play();
        GM.CallRotationWeapon();
        yield return new WaitForSeconds(0.05f);
        AM.EnemyTakeDamage.Play();
        yield return new WaitForSeconds(0.5f);
        Destroy(Damage);
        yield return StartCoroutine(MoveToPos(originalPos));
        animator.SetFloat("xvelocity", 0);
        animator.SetFloat("yvelocity", 0);
        isPlayerTurn = false;
        isTurn = false;
    }

    IEnumerator CDBeforeDie()
    {
        dissolveAmount += Time.deltaTime;
        dissolveAmount = Mathf.Clamp(dissolveAmount, 0, 1.1f);
        spriteRenderer.material.SetFloat("_DissolveAmount", dissolveAmount);
        yield return new WaitForSeconds(1f);
        GM.gameOver();
    }

}
