using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bunker : MonoBehaviour
{
    [Header("Health")]
    public float CurrentHealth;
    public float MaxHealth;
    public GameObject HealthBar;
    Slider Slider_HealthBar;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        Slider_HealthBar = HealthBar.GetComponent<Slider>();
        Slider_HealthBar.maxValue = MaxHealth;
        Slider_HealthBar.value = CurrentHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentHealth <= 0f)
        {
            Destroy(gameObject);
            Time.timeScale = 0;
        }
    }

    public void TakeDamage(float Damage)
    {
        Debug.Log(Damage + "dégats");
        CurrentHealth = CurrentHealth -= Damage;
        Slider_HealthBar.value = CurrentHealth;
    }
}
