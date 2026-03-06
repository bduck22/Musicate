using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class judgement : MonoBehaviour
{
    data_script data;
    AudioSource audio;
    public float speed;
    public TMP_Text text;
    TMP_Text combo;
    public bool[] long_wait;
    int gear_wid;
    public GameObject[] booooom;
    judgeani Judge;
    judgeani Combo;
    GameObject judgeob;
    bool firstnote;
    void Start()
    {
        firstnote = true;
        judgeob = GameObject.FindWithTag("first");
        Judge = text.gameObject.GetComponent<judgeani>();
        combo = GameObject.FindWithTag("canvas").transform.GetChild(1).GetComponent<TMP_Text>();
        Combo = combo.gameObject.GetComponent<judgeani>();
        long_wait = new bool[5];
        for (int i = 0; i < 5; i++) long_wait[i] = false;
        audio = gameObject.GetComponent<AudioSource>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        speed = data.speed;
        switch (data.po)
        {
            case gear_po.left: gear_wid = -11; break;
            case gear_po.mid: gear_wid = 0; break;
            case gear_po.right: gear_wid = 11; break;
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)0]))
        {
            judge(1);
        }
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)1]))
        {
            judge(2);
        }
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)2]) || Input.GetKeyDown(KeySetting.keys[(keyAction)3]))
        {
            if (!long_wait[2])
            {
                judge(3);
            }
        }
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)4]))
        {
            judge(4);
        }
        if (Input.GetKeyDown(KeySetting.keys[(keyAction)5]))
        {
            judge(5);
        }
        if (long_wait[0])
        {
            if (Input.GetKeyUp(KeySetting.keys[(keyAction)0]))
            {
                judge(1);
            }
        }
        if (long_wait[1])
        {
            if (Input.GetKeyUp(KeySetting.keys[(keyAction)1]))
            {
                judge(2);
            }
        }
        if (long_wait[2])
        {
            if (Input.GetKeyUp(KeySetting.keys[(keyAction)2]) || Input.GetKeyUp(KeySetting.keys[(keyAction)3]))
            {
                if(!Input.GetKey(KeySetting.keys[(keyAction)2]) && !Input.GetKey(KeySetting.keys[(keyAction)3]))
                {
                    judge(3);
                }
            }
        }
        if (long_wait[3])
        {
            if (Input.GetKeyUp(KeySetting.keys[(keyAction)4]))
            {
                judge(4);
            }
        }
        if (long_wait[4])
        {
            if (Input.GetKeyUp(KeySetting.keys[(keyAction)5]))
            {
                judge(5);
            }
        }

        //judge(1);
        //judge(2);
        //judge(3);
        //judge(4);
        //judge(5);
    }
    void judge(int line)
    {
        int linenum = line;
        GameObject note;
        GameObject[] objecct = GameObject.FindGameObjectsWithTag($"line{line}");
        switch (line)
        {
            case 1: line = -4; break;
            case 2: line = -2; break;
            case 3: line = 0; break;
            case 4: line = 2; break;
            case 5: line = 4; break;
        }
        if (objecct.Length > 0)
        {
            note = objecct[0];
            float shortDis = Vector2.Distance(new Vector2(line, -5), objecct[0].transform.position);
            foreach (GameObject obj in objecct)
            {
                float Distance = Vector2.Distance(new Vector2(line, -5), obj.transform.position);
                if (Distance < shortDis)
                {
                    note = obj;
                    shortDis = Distance;
                }
            }
            if (!note.GetComponent<Long>())
            {
                long_wait[linenum - 1] = false;
            }
            bool longend = false;
            float ftime = note.transform.position.y;
            if (0.235f > Mathf.Abs(ftime) / speed)//ftime - 0.235f < -judgeob.transform.position.y/speed
            {
                if (note.GetComponent<Long>())
                {
                    if (note.GetComponent<Long>().type)
                    {
                        if (!long_wait[linenum - 1])
                        {
                            if (!note.GetComponent<move1>().enabled) return;
                            StartCoroutine(Judge.ani());
                            StartCoroutine(Combo.ani());
                            booooom[linenum - 1].GetComponent<boooomoff>().stop = true;
                            StartCoroutine(booooom[linenum - 1].GetComponent<boooomoff>().repeat());
                            long_wait[linenum - 1] = true;
                            note.transform.position = new Vector3(line + gear_wid, 0, 0.2f);
                            note.GetComponent<move1>().enabled = false;
                        }
                        else
                        {
                            booooom[linenum - 1].GetComponent<boooomoff>().stop = false;
                            long_wait[linenum - 1] = false;
                            ftime = note.GetComponent<Long>().end.transform.position.y;
                            if (0.185f <= Mathf.Abs(ftime) / speed)//ftime - 0.235f >= -judgeob.transform.position.y/speed
                            {
                                data.combo = 0;
                                text.text = "MISS";//�ճ�Ʈ�ʹ�������
                                data.judge[5]++;
                                text.color = new Color(255, 0, 0);
                            }
                            longend = true;
                            Destroy(note.GetComponent<Long>().end);
                            Destroy(note);
                        }
                    }
                    else if (long_wait[linenum - 1])
                    {
                        booooom[linenum - 1].GetComponent<boooomoff>().stop = false;
                        long_wait[linenum - 1] = false;
                    }
                }

                if (0.235f > Mathf.Abs(ftime) / speed)
                {
                    if (firstnote)
                    {
                        firstnote = false;
                        for (float i = 0, k = 0; i < data.fullcombo; i++)
                        {
                            k += 1000000 / data.fullcombo;
                            if (i == data.fullcombo - 1)
                            {
                                data.score = 1000000f - k;
                            }
                        }
                    }
                    float time = Mathf.Abs(ftime) / speed - 0.01f;
                    //time = 0;
                    if (Mathf.Abs(time) < 0.043f / (longend ? 2.1f : 1))//43ms
                    {
                        data.score += 1000000 / data.fullcombo;
                        data.combo++;
                        text.text = "PERFECT";
                        data.judge[0]++;
                        text.color = new Color32(255, 255, 255, 255);
                    }
                    else if (Mathf.Abs(time) < 0.093f / (longend ? 2.1f : 1))//50ms
                    {
                        data.score += 1000000 / data.fullcombo * 0.8f;
                        data.combo++;
                        text.text = "GREAT";
                        data.judge[1]++;
                        text.color = new Color(255, 204, 0);
                    }
                    else if (Mathf.Abs(time) < 0.148f / (longend ? 2.1f : 1))//55ms
                    {
                        data.score += 1000000 / data.fullcombo * 0.5f;
                        data.combo++;
                        text.text = "CLEAR";
                        data.judge[2]++;
                        text.color = new Color32(0, 255, 0, 255);
                    }
                    else if (Mathf.Abs(time) < 0.203f / (longend ? 2.1f : 1))//55ms
                    {
                        data.score += 1000000 / data.fullcombo * 0.3f;
                        data.combo++;
                        text.text = "BAD";
                        data.judge[3]++;
                        text.color = new Color32(0, 51, 153, 255);
                    }
                    else if (Mathf.Abs(time) < 0.235f / (longend ? 2.1f : 1))//32ms
                    {
                        data.score++;
                        data.combo++;
                        data.judge[4]++;
                        text.text = "HIT";
                        text.color = Color.gray;
                    }
                    else
                    {
                        data.combo = 0;
                        data.judge[5]++;
                        text.text = "MISS";
                        text.color = new Color(255, 0, 0);
                    }
                    if (!note.GetComponent<Long>() || note.GetComponent<Long>() && !note.GetComponent<Long>().type)
                    {
                        if (Mathf.Abs(time) < 0.235f)
                        {
                            if (Mathf.Abs(time) < 0.203f) StartCoroutine(Judge.ani());
                            StartCoroutine(Combo.ani());
                            booooom[linenum - 1].SetActive(true);
                        }
                        Destroy(note);
                    }
                }
                if (note.GetComponent<Long>())
                {
                    if (!note.GetComponent<Long>().type)
                    {
                        if (long_wait[linenum - 1])
                        {
                            long_wait[linenum - 1] = false;
                        }
                    }
                }
            }
        }
    }
}
