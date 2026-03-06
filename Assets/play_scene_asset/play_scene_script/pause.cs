using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause : MonoBehaviour
{
    public GameObject[] button;
    int bnum;
    void Start()
    {
        bnum = 0;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (--bnum < 0) bnum = 2;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (++bnum > 2) bnum = 0;
        }
        for (int i = 0; i < 3; i++)
        {
            button[i].GetComponent<TMP_Text>().color = new Color32(255, 255, 255, 255);
        }
        button[bnum].GetComponent<TMP_Text>().color = new Color32(0, 200, 255, 255);
        if (Input.GetKeyDown(KeyCode.Return))
        {
            switch (bnum)
            {
                case 0: GameObject.FindWithTag("gameobject").GetComponent<stop>().resume(); break;
                case 1: SceneManager.LoadScene(3); break;
                case 2: SceneManager.LoadScene(2); break;
            }
        }
    }
}
