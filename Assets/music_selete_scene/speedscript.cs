using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class speedscript : MonoBehaviour
{
    data_script data;
    TMP_Text text;
    void Start()
    {
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        text.text = "Speed\n"+ ((data.speed - 3) / 5 + 1).ToString("0.0");
    }
}
