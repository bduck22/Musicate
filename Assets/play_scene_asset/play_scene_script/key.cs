using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using static Unity.VisualScripting.Member;

public class key : MonoBehaviour
{
    void OnGUI()
    {
        bool no = true;
        Event KeyEvent = Event.current;
        if (KeyEvent.isKey)
        {
            for (int i = 0; i < (int)keyAction.Keycount; i++)
            {
                if (KeyEvent.keyCode == KeySetting.keys[(keyAction)i])
                {
                    no = false;
                }
            }
            if(no) KeySetting.keys[(keyAction)Key] = KeyEvent.keyCode;
            StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
            Key = -1;
        }
    }
    int Key = -1;
    public void ChangeKey(int num)
    {
        Key = num;
    }
}