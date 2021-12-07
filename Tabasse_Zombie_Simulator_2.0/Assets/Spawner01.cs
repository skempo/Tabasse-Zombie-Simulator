using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner01 : MonoBehaviour
{
    [Header("Spawner")]
    public GameObject spawnEnnemy;
    [SerializeField] float MinTimeSpawn;
    [SerializeField] float MaxTimeSpawn;
    float timeSpawn;
    float time;

    [Header("Movement")]
    [SerializeField] float speed;
    private float step;

    
    
    [HideInInspector] public Vector2 TSpawn1Pos ;
    [HideInInspector] public  Vector2 TSpawn2Pos;

    [HideInInspector] public Vector2 TargetPos; // initialiser par spawnermanager 

    enum SpawnerType { topSpawner, botSpawner, leftSpawner, rightSpawner}
    [SerializeField] SpawnerType spawnerType;


    // Start is called before the first frame update
    void Start()
    {
        time = 0f;
        timeSpawn = Random.Range(MinTimeSpawn, MaxTimeSpawn);

        
        //je vais chercher le script du managerSpawner 
        ManagerSpawnScript managerSpawnScript = transform.parent.GetComponent<ManagerSpawnScript>();

        //defini les target de chaque spawner 
        switch (spawnerType)
        {
            case SpawnerType.topSpawner:
                TSpawn1Pos = managerSpawnScript.TSpawnAPos;
                TSpawn2Pos = managerSpawnScript.TSpawnBPos;
                break;

            case SpawnerType.botSpawner:
                TSpawn1Pos = managerSpawnScript.TSpawnCPos;
                TSpawn2Pos = managerSpawnScript.TSpawnDPos;
                break;

            case SpawnerType.leftSpawner:
                TSpawn1Pos = managerSpawnScript.TSpawnAPos;
                TSpawn2Pos = managerSpawnScript.TSpawnCPos;
                break;

            case SpawnerType.rightSpawner:
                TSpawn1Pos = managerSpawnScript.TSpawnBPos;
                TSpawn2Pos = managerSpawnScript.TSpawnDPos;
                break;

        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        

        //Verifie si le spawner se rapproche d'un target 
        if (Vector2.Distance(transform.position, TSpawn1Pos) <= 0.5)
        {
            TargetPos = TSpawn2Pos;
        }
        if (Vector2.Distance(transform.position, TSpawn2Pos) <= 0.5)
        {
            TargetPos = TSpawn1Pos;
        }

        MovetoTarget();
        SpawnEnnemy();
    }

    void MovetoTarget()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, TargetPos, step);
    }

    void SpawnEnnemy()
    {
        time = time + Time.deltaTime;

        if (time >= timeSpawn)
        {
            time = 0f;
            Instantiate(spawnEnnemy, transform.position, transform.rotation);

        }
    }

    
}
