using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Keyprint : MonoBehaviour
{
    public TMP_Text[] txt;
    void Start()
    {
        StartCoroutine(settingkey());

    }
    IEnumerator settingkey()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < txt.Length; i++)
        {
            txt[i].text = KeySetting.keys[(keyAction)i].ToString();
        }
    }
    void Update()
    {
        if (txt[0].gameObject.activeSelf)StartCoroutine (settingkey());
    }
}   