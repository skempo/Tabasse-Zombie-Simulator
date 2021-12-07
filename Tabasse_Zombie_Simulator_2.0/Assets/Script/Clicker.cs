using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    

    public GameObject ClickTrigger;

    [SerializeField]  LayerMask ClickLayer;

    Vector3 positioncirlce = new Vector3(0, 0, 0);

    [HideInInspector] public bool canClick = true;

    private void Awake()
    {
        // convertie la valeur du layer mask en la veritable valeur du layer de l'inspecteur 
        ClickLayer.value = Mathf.RoundToInt(Mathf.Log(ClickLayer.value, 2));
        
            
       
    }
    // Update is called once per frame
    void Update()
    {
        if (canClick)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector3 CLick = Input.mousePosition;
                Vector3 ClickPos = Camera.main.ScreenToWorldPoint(CLick);
                ClickPos.z = 0f;
                positioncirlce = ClickPos;

                //Instantiate(ClickTrigger, ClickPos, transform.rotation);
                //Debug.Log(Mathf.Sqrt( ClickLayer.value));
                Debug.Log(ClickLayer.value);

                // convertie la valeur du layer mask en la veritable valeur du layer de l'inspecteur
                int enemyLayerInt = ClickLayer.value = Mathf.RoundToInt(Mathf.Log(ClickLayer.value, 2));

                RaycastHit2D[] EnnemyTouched = Physics2D.CircleCastAll(ClickPos, 0.2f, transform.forward, enemyLayerInt);

                if (EnnemyTouched.Length > 0)
                {
                    EnnemyTouched[0].transform.GetComponent<Ennemy>().TakeDamage(1);

                }

            }

        }

    }

    
}

    
