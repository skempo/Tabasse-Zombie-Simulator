using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner01 : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject spawnEnnemy;
    public float DeltatimeSpawn;
    private float temps;

    [Header("Movement")]
    public float speed;
    private float step;

    public GameObject SpawnA;
    public GameObject SpawnB;
    private Vector2 SpawnAPos;
    private Vector2 SpawnBPos;

    private Vector2 TargetPos;

    // Start is called before the first frame update
    void Start()
    {
        temps = 0f;
        SpawnAPos = SpawnA.transform.position;
        SpawnBPos = SpawnB.transform.position;

        TargetPos = SpawnAPos;
    }

    // Update is called once per frame
    void Update()
    {
        temps = temps + Time.deltaTime;

        if(temps >= DeltatimeSpawn)
        {
            temps = 0f;
           Instantiate(spawnEnnemy, transform.position, transform.rotation);
           
        }

        //Verifie si le spawner se rapproche d'un target 
        if (Vector2.Distance(transform.position,SpawnAPos) <= 0.5)
        {
            TargetPos = SpawnBPos;
        }
        if (Vector2.Distance(transform.position, SpawnBPos) <= 0.5)
        {
            TargetPos = SpawnAPos;
        }

        MovetoTarget();
    }

    void MovetoTarget()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, step);
    }

    
}
