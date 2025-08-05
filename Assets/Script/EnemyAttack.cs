using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private GameObject Player;
    private float moveSpeed = 9f;
    private GameManager GM;

    private PlayerAttack PA;
    
    private AudioManager AM;
    public GameObject DamageUI;
    private Transform canvasPos;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        GM = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        PA = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();
        AM = GameObject.FindWithTag("AudioManager").GetComponent<AudioManager>();
        
        canvasPos = GameObject.FindWithTag("CanvasPos").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public IEnumerator MoveAndBack()
    {
        Vector2 enemyPos = transform.position;

        Vector2 playerPos = (Vector2)Player.transform.position + new Vector2(1, 0);
        Vector2 enemyBack = (Vector2)enemyPos + new Vector2(0.3f, 0);

        yield return StartCoroutine(MoveToPos(playerPos, gameObject));
        GameObject Damage = Instantiate(DamageUI, playerPos + new Vector2(-1.8f, -0.3f), Quaternion.identity, canvasPos);
        TextMeshProUGUI DamageText = Damage.GetComponent<TextMeshProUGUI>();
        DamageText.text = "10";
        PA.health -= 10f;
        AM.EnemyAttack.Play();
        yield return new WaitForSeconds(0.5f);
        AM.PlayerTakeDamage.Play();
        Destroy(Damage);
        yield return StartCoroutine(MoveToPos(enemyBack, gameObject));
    }
}