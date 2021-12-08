using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Timeline;

public class Ennemy : MonoBehaviour
{
    [Header("Health Variables")]
    int CurrentHealth;
    [SerializeField] int MaxHealth;


    [Header("Movement Variables")]
    [SerializeField] Vector2 target;
    [SerializeField] float speed;
    float slowedSpeed;
    private float step;

    GameObject Bunker_gameObject;
    Bunker BunkerScript;

    [Header("Attack Variables")]
    [SerializeField] float myDamage;
    [SerializeField] float Cooldown;
    float time_Cooldown = 0f;
    bool CanAttack = true;

    Animator myAnim;

    [Header("HealthCount")]
    [SerializeField] GameObject HealthCountPrefab;
    [SerializeField] Transform transformHealthCount;
    GameObject myHealthCount;
    Transform transformCanvas;

    GameObject myCollisionObject;

    [Header("FeedbackTouched")]
    bool onFeedbackTouched = false;
    float timerFeedbackTouched = 0;
    [SerializeField] Color BasicColor;
    [SerializeField] Color TouchedColor;

    [SerializeField] float MaxTimeSlow;
    float timeSlow = 0f;
    bool isSlowingDown = false;

    private void Awake()
    {
        //cherche le canvas dans la scene 
        transformCanvas = GameObject.Find("Canvas").transform;
    }
    private void Start()
    {
        myAnim = GetComponent<Animator>();
        CurrentHealth = MaxHealth;

        slowedSpeed = speed / 2;

        //cible le bunker au centre de la scene 
        target = new Vector2(0f, 0f);

        //instantie le decompte de PV
        myHealthCount = Instantiate(HealthCountPrefab, transformHealthCount.position, Quaternion.identity,transformCanvas );
        myHealthCount.GetComponent<Text>().text = CurrentHealth.ToString();

        
        
    }


    private void Update()
    {
        if (!isSlowingDown) step = speed * Time.deltaTime;
        else
        {
            step = slowedSpeed * Time.deltaTime;
            if (timeSlow < MaxTimeSlow)
            {
                timeSlow += Time.deltaTime;
            }
            else isSlowingDown = false;
        } 
            
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        //quand Enemy Mort
        

        /*
        if(CanAttack == false)
        {
            CooldownAttack(Cooldown);
        }
        */
        myHealthCount.transform.position = transformHealthCount.position;

        /*
        if (onFeedbackTouched && timerFeedbackTouched < 0.1)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = TouchedColor;
            timerFeedbackTouched += Time.deltaTime;
        }
        else
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().color = BasicColor;
            timerFeedbackTouched = 0;
            onFeedbackTouched = false;
        }
        */

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bunker")
        {
            Bunker_gameObject = collision.gameObject;
            BunkerScript = Bunker_gameObject.GetComponent<Bunker>();
            BunkerScript.TakeDamage(myDamage);
            Death();


        }
    } 

    public void TakeDamage (int damage)
    {
        CurrentHealth -= damage;
        myAnim.SetTrigger("Take It");
        //onFeedbackTouched = true;
        myHealthCount.GetComponent<Text>().text = CurrentHealth.ToString();

        isSlowingDown = true;
        timeSlow = 0f;

        if (CurrentHealth <= 0)
        {
            MANAGER.Instance.AddScore(200);
            Death();
        }
    }

    void Death()
    {
        Destroy(gameObject);
        Destroy(myHealthCount);
    }
    /*
    private void OnCollisionStay2D(Collision2D collision)
    {
        
        if(collision.gameObject.tag == "Bunker")
        {
            
            Bunker_gameObject = collision.gameObject;
            BunkerScript = Bunker_gameObject.GetComponent<Bunker>();
            

            speed = 0f;

            if (CanAttack == true)
            {
                
                BunkerScript.TakeDamage(Damage);
                CanAttack = false;
                
            }
          
        }
    }
    
    
    void CooldownAttack(float Cooldown)
    {
        time_Cooldown = time_Cooldown += Time.deltaTime;

        if(time_Cooldown >= Cooldown)
        {
            CanAttack = true;
            time_Cooldown = 0f;
        }

    }
    */
    
}
