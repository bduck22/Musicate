using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class livejudge : MonoBehaviour
{
    data_script data;
    bms_test bms;
    TMP_Text songname;
    TMP_Text artist;
    TMP_Text bpm;
    TMP_Text diffi;

    TMP_Text acc;
    TMP_Text[] judge;
    TMP_Text tear;
    string[] judgestring;
    void Start()
    {
        judgestring = new string[6];
        judgestring[0] = "PERFECT";
        judgestring[1] = "GREAT";
        judgestring[2] = "CLEAR";
        judgestring[3] = "BAD";
        judgestring[4] = "HIT";
        judgestring[5] = "MISS";
        judge = new TMP_Text[6];
        bms = GameObject.FindWithTag("gameobject").GetComponent<bms_test>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        songname = transform.GetChild(1).GetComponent<TMP_Text>();
        artist = transform.GetChild(2).GetComponent<TMP_Text>();
        bpm = transform.GetChild(4).GetComponent<TMP_Text>();
        diffi = transform.GetChild(3).GetComponent<TMP_Text>();
        acc = transform.GetChild(6).GetComponent<TMP_Text>();
        tear = transform.GetChild(5).GetChild(0).GetComponent<TMP_Text>();
        for(int i = 0; i < 6; i++)
        {
            judge[i] = transform.GetChild(0).GetChild(i).GetComponent<TMP_Text>();
        }
    }
    void Update()
    {
        songname.text = data.song_list[data.nowsong].name;
        artist.text = data.song_list[data.nowsong].writer;
        bpm.text = bms.playsong.bpm.ToString()+" BPM";
        string diffiname = "";
        Color color;
        switch (data.nowdiffi)
        {
            case 0:diffiname = "BASIC"; ColorUtility.TryParseHtmlString("#35E828", out color); diffi.color = color; break;
            case 1: diffiname = "MEDIUM"; ColorUtility.TryParseHtmlString("#FFF300", out color); diffi.color = color; break;
            case 2: diffiname = "EXPERT"; ColorUtility.TryParseHtmlString("#FF0011", out color); diffi.color = color; break;
            case 3: diffiname = "CHAOS"; ColorUtility.TryParseHtmlString("#8A00FF", out color); diffi.color = color; break;
        }
        diffi.text = diffiname + " " + bms.playsong.level;
        float accnum = Mathf.Floor(data.score / 10000 * 100) / 100;
        acc.text = accnum.ToString("0.00") + "%";
        if (accnum >= 60)
        {
            if (accnum >= 70)
            {
                if (accnum >= 80)
                {
                    if (accnum >= 90)
                    {
                        if (accnum >= 95)
                        {
                            if (accnum <= 96) tear.text = "S-";
                            else if (accnum <= 98) tear.text = "S";
                            else if (accnum <= 99) tear.text = "S+";
                            else if (accnum <= 100) tear.text = "SS";
                        }
                        else if (accnum <= 91.5f) tear.text = "A-";
                        else if (accnum <= 93.5f) tear.text = "A";
                        else tear.text = "A+";
                    }
                    else if (accnum < 83.33f) tear.text = "B-";
                    else if (accnum > 86.66f) tear.text = "B+";
                    else tear.text = "B";
                }
                else if (accnum < 73.33f) tear.text = "C-";
                else if (accnum > 76.66f) tear.text = "C+";
                else tear.text = "C";
            }
            else
            {
                if (accnum < 63.33f) tear.text = "D-";
                else if (accnum > 66.66f) tear.text = "D+";
                else tear.text = "D";
            }
        }
        else
        {
            tear.text = "F";
        }
        for (int i = 0; i < 6; i++)
        {
            judge[i].text = judgestring[i] + "\n" + data.judge[i].ToString();
        }
    }
}
