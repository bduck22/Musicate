using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyboom : MonoBehaviour
{
    public GameObject[] boom;
    bms_test bms;
    void Start()
    {
        bms = GameObject.FindWithTag("gameobject").GetComponent<bms_test>();
    }
    void Update()
    {
        if (Cursor.visible) Cursor.visible = false;
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)0]))
        {
            boom[0].SetActive(true);
        }
        else if (Input.GetKey(KeySetting.keys[(keyAction)0]))
        {
            boom[0].SetActive(true);
        }
        else boom[0].SetActive(false);

        if (Input.GetKeyDown(KeySetting.keys[(keyAction)1]))
        {
            boom[1].SetActive(true);
        }
        else if (Input.GetKey(KeySetting.keys[(keyAction)1]))
        {
            boom[1].SetActive(true);
        }
        else boom[1].SetActive(false);

        if (Input.GetKeyDown(KeySetting.keys[(keyAction)2]) || Input.GetKeyDown(KeySetting.keys[(keyAction)3]))
        {
            boom[2].SetActive(true);
        }
        else if (Input.GetKey(KeySetting.keys[(keyAction)2]) || Input.GetKey(KeySetting.keys[(keyAction)3]))
        {
            boom[2].SetActive(true);
        }
        else boom[2].SetActive(false);

        if (Input.GetKeyDown(KeySetting.keys[(keyAction)4]))
        {
            boom[3].SetActive(true);
        }
        else if (Input.GetKey(KeySetting.keys[(keyAction)4]))
        {
            boom[3].SetActive(true);
        }
        else boom[3].SetActive(false);

        if (Input.GetKeyDown(KeySetting.keys[(keyAction)5]))
        {
            boom[4].SetActive(true);
        }
        else if (Input.GetKey(KeySetting.keys[(keyAction)5]))
        {
            boom[4].SetActive(true);
        }
        else boom[4].SetActive(false);
    }
}