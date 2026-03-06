using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class save_test : MonoBehaviour
{
    data_script data;
    public Slider audioSlider;
    void Start()
    {
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        StartCoroutine(save_key_setting());
    }
    public IEnumerator save_key_setting()
    {
        yield return new WaitForSeconds(0.01f);
        string fileName = "/Setting.csv";
        string filepath = Application.dataPath + fileName;

        using(StreamWriter outStream = new StreamWriter(filepath)){
        string sb = "";
        for (int i = 0; i < (int)keyAction.Keycount; i++)
        {
            sb += ((int)KeySetting.keys[(keyAction)i]).ToString() + (i != (int)keyAction.Keycount - 1 ? "," : "");
        }
        outStream.WriteLine(sb);
            float sound = data.sound;
        sb = sound.ToString();
        outStream.WriteLine(sb);
        switch (data.po)
        {
            case gear_po.left: sb = "2"; break;
            case gear_po.right: sb = "1"; break;
            case gear_po.mid: sb = "0"; break;
        }
        outStream.WriteLine(sb);
        sb = data.nowsong.ToString();
        outStream.WriteLine(sb);
        sb = data.speed.ToString();
        outStream.WriteLine(sb);
        sb = data.nowgear + ","+data.nownote;
        outStream.WriteLine(sb);
        outStream.Close();
        }
        /*97,115,100,107,108,59
-5
0
0*/
    }
}
