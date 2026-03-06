using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class llock : MonoBehaviour
{

    void Update()
    {
        if(FindAnyObjectByType<bms_test>().start)
        {
            gameObject.GetComponent<move1>().enabled = true;
            gameObject.GetComponent<llock>().enabled = false;
        }
    }
}
