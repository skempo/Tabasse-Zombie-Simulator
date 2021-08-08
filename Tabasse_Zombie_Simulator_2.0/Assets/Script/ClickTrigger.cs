using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTrigger : MonoBehaviour
{
    private float Time2;

    // Start is called before the first frame update
    void Start()
    {
        Time2= 0f;
    }

    private void Update()
    {
        Time2 = Time2 += Time.deltaTime;
        ;

        if (Time2 >= 0.2f)
        {
            GameObject.Destroy(gameObject);
        }

    }

}
