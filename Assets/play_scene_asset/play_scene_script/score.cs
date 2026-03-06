using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class score : MonoBehaviour
{
    TMP_Text text;
    data_script data;
    void Start()
    {
        text = GetComponent<TMP_Text>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
    }
    void Update()
    {
        text.text = ((int)data.score).ToString();
    }
}
