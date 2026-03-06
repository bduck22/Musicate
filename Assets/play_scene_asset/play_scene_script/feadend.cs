using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class feadend : MonoBehaviour
{
    float color;
    void Start()
    {
    }
    void Update()
    {
        color = gameObject.GetComponent<Image>().color.a;
        if (color <=0)
        {
            StartCoroutine(GameObject.FindWithTag("gameobject").GetComponent<bms_test>().musingstart());
            gameObject.GetComponent<feadend>().enabled = false;
        }
    }
}
