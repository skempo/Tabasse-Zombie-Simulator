using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Timeline;

public class Ennemy : MonoBehaviour
{
    [Header("Health Variables")]
    public int CurrentHealth;
    public int MaxHealth;



    [Header("Movement Variables")]
    public Vector2 target;
    public float speed;
    private float step;

    GameObject Bunker_gameObject;
    Bunker BunkerScript;

    public float Damage;
    public float Cooldown;
    float time_Cooldown = 0f;
    bool CanAttack = true;



    Animator myAnim;


    private void Start()
    {
        myAnim = GetComponent<Animator>();
        CurrentHealth = MaxHealth;

        target = new Vector2(0f, 0f);

    }


    private void Update()
    {
        if (CurrentHealth <= 0)
        {
            GameObject.Destroy(gameObject);
        }

        if(CanAttack == false)
        {
            CooldownAttack(Cooldown);
        }

    }

    //détecte si touché par clicker
    private void OnTriggerEnter2D(Collider2D collision)
    {

        CurrentHealth -= 1 ;
        myAnim.SetTrigger("Take It");
    }

    //Movement
    private void FixedUpdate()
    {
        step = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, target, step);

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
