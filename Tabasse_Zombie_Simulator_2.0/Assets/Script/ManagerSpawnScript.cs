using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerSpawnScript : MonoBehaviour
{
    int randomDirSpawnerHor;
    int randomDirSpawnerVer;
    public GameObject SpawnA, SpawnB, SpawnC, SpawnD;
    
    [SerializeField] Transform spawnerTop;
    [SerializeField] Transform spawnerBot;
    [SerializeField] Transform spawnerLeft;
    [SerializeField] Transform spawnerRight;

    [HideInInspector] public Vector2 TSpawnAPos, TSpawnBPos, TSpawnCPos, TSpawnDPos;

    // Start is called before the first frame update
    void Awake()
    {
        //initialisation des cible des spawner 
        TSpawnAPos = SpawnA.transform.position;
        TSpawnBPos = SpawnB.transform.position;
        TSpawnCPos = SpawnC.transform.position;
        TSpawnDPos = SpawnD.transform.position;

        //Tirage au sort entre  et 2
        randomDirSpawnerHor = Random.Range(0, 2);
        randomDirSpawnerVer = Random.Range(0, 2);

        // je vais chercher le script de mes spawner 
        Spawner01 mySpawnerTopScript = spawnerTop.GetComponent<Spawner01>();
        Spawner01 mySpawnerBotScript = spawnerBot.GetComponent<Spawner01>();
        Spawner01 mySpawnerLeftScript = spawnerLeft.GetComponent<Spawner01>();
        Spawner01 mySpawnerRightScript = spawnerRight.GetComponent<Spawner01>();
        
        

        switch (randomDirSpawnerHor)
        {
            //dis a spawner top d'aller a droite et spawner bot à gauche 
            case 0:
                mySpawnerTopScript.TargetPos = TSpawnAPos;
                mySpawnerBotScript.TargetPos = TSpawnDPos;
                Debug.Log(mySpawnerTopScript.TargetPos);
                break;

            //l'inverse d'au dessus 
            case 1:
                mySpawnerTopScript.TargetPos = TSpawnBPos;
                mySpawnerBotScript.TargetPos = TSpawnCPos;
                break;
        }
        
        switch (randomDirSpawnerVer)
        {
            //dis a spawner top d'aller a droite et spawner bot à gauche 
            case 0:
                mySpawnerLeftScript.TargetPos = TSpawnAPos;
                mySpawnerRightScript.TargetPos = TSpawnDPos;
                Debug.Log(mySpawnerTopScript.TargetPos);
                break;

            //l'inverse d'au dessus 
            case 1:
                mySpawnerLeftScript.TargetPos = TSpawnCPos;
                mySpawnerRightScript.TargetPos = TSpawnBPos;
                break;
        }

    }

 
}
