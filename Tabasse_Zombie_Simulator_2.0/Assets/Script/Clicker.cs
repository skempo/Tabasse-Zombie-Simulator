using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clicker : MonoBehaviour
{
    public GameObject ClickTrigger;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 CLick = Input.mousePosition;
            Vector3 ClickPos = Camera.main.ScreenToWorldPoint(CLick);
            ClickPos.z = 0f;


            Instantiate(ClickTrigger, ClickPos, transform.rotation);

        }




    }
}
