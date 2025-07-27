using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyData data;
    [HideInInspector] public float CurrentHealth;
    private SpriteRenderer SpriteRenderer;
    private float dissolveAmount = 0;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        if (data != null)
        {
            CurrentHealth = data.maxHealth;
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
        Destroy(gameObject);
    }

}
