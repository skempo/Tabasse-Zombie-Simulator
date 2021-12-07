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
    public Vector2 target;
    public float speed;
    private float step;

    GameObject Bunker_gameObject;
    Bunker BunkerScript;

    [Header("Attack Variables")]
    [SerializeField] float Damage;
    [SerializeField] float Cooldown;
    float time_Cooldown = 0f;
    bool CanAttack = true;

    Animator myAnim;

    [Space(10)]
    [SerializeField] GameObject HealthCountPrefab;
    [SerializeField] Transform transformHealthCount;
    GameObject myHealthCount;
    Transform transformCanvas;

    GameObject myCollisionObject;

    bool onFeedbackTouched = false;
    float timerFeedbackTouched = 0;
    [SerializeField] Color BasicColor;
    [SerializeField] Color TouchedColor;

    private void Awake()
    {
        //cherche le canvas dans la scene 
        transformCanvas = GameObject.Find("Canvas").transform;
    }
    private void Start()
    {
        myAnim = GetComponent<Animator>();
        CurrentHealth = MaxHealth;

        //cible le bunker au centre de la scene 
        target = new Vector2(0f, 0f);

        //instantie le decompte de PV
        myHealthCount = Instantiate(HealthCountPrefab, transformHealthCount.position, Quaternion.identity,transformCanvas );
        myHealthCount.GetComponent<Text>().text = CurrentHealth.ToString();

        
        
    }


    private void Update()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);

        //quand Enemy Mort
        if (CurrentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
            Destroy(myHealthCount);
            MANAGER.Instance.AddScore(200);
            
        }

        
        if(CanAttack == false)
        {
            CooldownAttack(Cooldown);
        }

        myHealthCount.transform.position = transformHealthCount.position;


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


    }
    /*
    //détecte si touché par clicker
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (myCollisionObject == null && collision.tag == "ClickTrigger")
        {
            myCollisionObject = collision.gameObject;
            CurrentHealth -= 1;
            myAnim.SetTrigger("Take It");
            myHealthCount.GetComponent<Text>().text = CurrentHealth.ToString();
            
            Destroy(collision.gameObject);
        }
       
    }
    */
    public void TakeDamage (int damage)
    {
        CurrentHealth -= damage;
        myAnim.SetTrigger("Take It");
        onFeedbackTouched = true;
        myHealthCount.GetComponent<Text>().text = CurrentHealth.ToString();
    }

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

    
}
