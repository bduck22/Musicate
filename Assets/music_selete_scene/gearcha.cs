using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gearcha : MonoBehaviour
{
    data_script data;
    void Start()
    {
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
    }
    public void changegearwid(int a)
    {
        data.gearchange(a);
    }
}