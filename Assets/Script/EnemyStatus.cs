using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : MonoBehaviour
{
    public EnemyData data;
    [HideInInspector] public float CurrentHealth;
    // Start is called before the first frame update
    void Start()
    {
        if (data != null)
        {
            CurrentHealth = data.maxHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
