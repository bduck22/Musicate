using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class Line
{
    public int type;
    public float bit;
    public int[] nowbit;
    public int line;
    public int[] power;
    public void set(int type, float bit, int[] nowbit, int line, int[] power)
    {
        this.type = type;
        this.bit = bit;
        this.nowbit = nowbit;
        this.line = line;
        this.power = power;
    }
}
public class Bar
{
    public List<Line> notes;
}
public class Song
{
    public string title;
    public string artist;
    public float bpm;
    public int level;
    public int diffi;
    public List<Bar> bars;
    public float measure;
}
enum read_sta { head, main}
public class bms_test : MonoBehaviour
{
    public TMP_Text judge;
    public TMP_Text combo;
    public TMP_Text score;
    data_script data;
    public GameObject note;
    public GameObject longnote;
    public GameObject bar;
    public GameObject load;
    public float speed;
    public Song playsong = new Song();
    public bool start;
    int gear_wid;
    AudioSource audio;
    public GameObject gear;
    bool[] long_wait;
    public Image background;
    public GameObject info;
    GameObject[] longobj;
    public GameObject[] effect;
    read_sta now = new read_sta();

    public float TickMs;
    void Start()
    {
        Time.timeScale = 1;
        long_wait = new bool[5];
        longobj = new GameObject[5];
        gameObject.GetComponent<judgement>().speed = speed;
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        data.init();
        speed = data.speed;
        float xxxx=0;
        switch (data.po)
        {
            case gear_po.left: gear_wid = -11; xxxx = 2f; break;
            case gear_po.mid: gear_wid = 0; xxxx = 12.7639f; break;
            case gear_po.right: gear_wid = 11; xxxx = -2f; break;
        }
        Color color = new Color();
        for (int i = 0; i < 5; i++)
        {
            effect[i].GetComponent<Animator>().runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>($"BOOOOOOOOM/{data.nownote}");
            if (i % 2 == 0)
            {
                switch (data.nownote)
                {
                    case 1: ColorUtility.TryParseHtmlString("#00C7FF", out color); break;
                    case 2: ColorUtility.TryParseHtmlString("#00FFFC", out color); break;
                }
            }else
            {
                switch (data.nownote)
                {
                    case 1: ColorUtility.TryParseHtmlString("#FF9500", out color); break;
                    case 2: ColorUtility.TryParseHtmlString("#C03BC6", out color); break;
                }
            }
            effect[i].GetComponent<SpriteRenderer>().color = color;
        }
        gear.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite= Resources.Load<Sprite>($"skin/gear{data.nowgear}");
        gear.transform.position = new Vector2(gear_wid, 0);
        judge.transform.position = Camera.main.WorldToScreenPoint(new Vector2(gear_wid, 10.525f));
        combo.transform.position = Camera.main.WorldToScreenPoint(new Vector2(gear_wid, 12.67f));
        score.transform.position = Camera.main.WorldToScreenPoint(new Vector2(gear_wid, -5.75f));
        info.transform.position = Camera.main.WorldToScreenPoint(new Vector2(xxxx, 7.0943f));
        audio = gameObject.GetComponent<AudioSource>();
        audio.Play();
        audio.volume = 0;
        audio.resource = Resources.Load<AudioResource>($"{data.nowsong_name}_AU");
        //audio.resource = Resources.Load<AudioResource>($"����(����)_AU");//����(����)_AU
        start = false;
        playsong.bars = new List<Bar>();
        string filepath;
        #if UNITY_EDITOR
                string fileName = $"/bms/{data.nowsong_name}_{data.nowdiffi_name}.bms";
        filepath = Application.dataPath + fileName;
        #endif
        filepath = Application.streamingAssetsPath + $"/bms/{data.nowsong_name}_{data.nowdiffi_name}.bms";

        string files = File.ReadAllText(filepath);

        string[] bmsData = files.Split('\n');
        //string[] bmsData = File.ReadAllLines($"Assets/Resources/bms/{data.nowsong_name}_{data.nowdiffi_name}.bms");
        //string[] bmsData = File.ReadAllLines($"Assets/Resources/bms/����(����)_star.bms");
        background.sprite = Resources.Load<Sprite>($"{data.nowsong_name}_IMAGE");
        load.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{data.nowsong_name}_IMAGE");
        foreach (string sr in bmsData)
        {
            if (!string.IsNullOrWhiteSpace(sr))
            {
                //Debug.Log(sr.Trim() == "*---------------------- HEADER FIELD");
                //Debug.Log("*---------------------- HEADER FIELD");
                //Debug.Log("asdfs"+sr+"asdf");
                if (sr.Trim() == "*---------------------- HEADER FIELD")
                {
                    now = read_sta.head;
                }
                else if (sr.Trim() == "*---------------------- MAIN DATA FIELD") now = read_sta.main;
                else if (now == read_sta.head)
                {
                    string[] data = sr.Split('#')[1].Split(' ');
                    if (data[0] == "TITLE") playsong.title = data[1];
                    if (data[0] == "ARTIST") playsong.artist = data[1];
                    if (data[0] == "BPM") playsong.bpm = int.Parse(data[1]);
                    if (data[0] == "PLAYLEVEL") playsong.level = int.Parse(data[1]);
                    if (data[0] == "DIFFICULTY") playsong.diffi = int.Parse(data[1]) - 2;
                }
                else if (now == read_sta.main)
                {
                    int type = -1;
                    int line;
                    int[] nowbit;
                    int[] power = null;
                    string[] data = sr.Split('#')[1].Split(':');
                    int bar = int.Parse(data[0].Substring(0, 3));
                    switch (int.Parse(data[0].Substring(3, 1)))
                    {
                        case 1: type = 0; break;//�Ϲݳ�Ʈ
                        case 5: type = 1; break;//�ճ�Ʈ
                        case 0:
                            {
                                switch (int.Parse(data[0].Substring(4, 1)))
                                {
                                    case 1: type = 3; break;//BGM
                                    case 3: type = 4; break;//BPM
                                    case 9: type = 5; break;//STOP
                                }
                                break;
                            }
                    }
                    int bit = data[1].Length / 2;
                    if (type == 1 || type == 0)
                    {
                        line = int.Parse(data[0].Substring(4, 1));
                        nowbit = new int[data[1].Split("01").Length - 1];
                        int i = 0;
                        foreach (string bitt in data[1].Split("01"))
                        {
                            if (i < nowbit.Length)
                            {
                                this.data.fullcombo++;
                                nowbit[i] = bitt.Length / 2 + (i != 0 ? nowbit[i - 1] + 1 : 0);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        line = -1;
                        int l = 0;
                        for (int i = 0; i < bit; i += 2)
                        {
                            int num = Convert.ToInt32(data[1].Substring(i, 2), 16);
                            if (num != 0)
                            {
                                l++;
                            }
                        }
                        nowbit = new int[l];
                        power = new int[l];
                        for (int i = 0, j = 0; i < bit; i += 2)
                        {
                            int num = Convert.ToInt32(data[1].Substring(i, 2), 16);
                            if (num != 0)
                            {
                                nowbit[j] = i / 2;
                                power[j] = num;
                                j++;
                            }
                        }
                    }
                    if (playsong.bars.Count < bar)
                    {
                        while (playsong.bars.Count < bar)
                        {
                            playsong.bars.Add(new Bar());
                            playsong.bars[playsong.bars.Count - 1] = new Bar();
                            playsong.bars[playsong.bars.Count - 1].notes = new List<Line>();
                        }
                    }
                    playsong.bars[bar - 1].notes.Add(new Line()); //������ ������ �κ�
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].bit = bit;
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].nowbit = nowbit;
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].type = type;
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].power = power;
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].line = line;

                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1] = new Line();
                    playsong.bars[bar - 1].notes[playsong.bars[bar - 1].notes.Count - 1].set(type, bit, nowbit, line, power);

                    //Debug.Log(playsong.bars[0].notes.Count);
                    //Debug.Log("��" + bar);
                    //Debug.Log("Ÿ��" + playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].type);
                    //Debug.Log("��Ʈ" + playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].bit);
                    //Debug.Log("����" + playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].line);
                    //foreach (int i in playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].nowbit)
                    //{
                    //    Debug.Log("��Ʈ ��ġ" + i);
                    //}
                    //if (playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].type != 0 && playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].type != 1)
                    //{
                    //    foreach (int i in playsong.bars[0].notes[playsong.bars[0].notes.Count - 1].power)
                    //    {
                    //        Debug.Log("�̺�Ʈ ��" + i);
                    //    }
                    //}
                    //Debug.Log("");
                }
            }
        }
        //for (int i = 0; i < playsong.bars.Count; i++)
        //{
        //    Debug.Log("?" + playsong.bars[i].notes.Count);
        //    Debug.Log("��" + i + 1);
        //    Debug.Log("Ÿ��" + playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].type);
        //    Debug.Log("��Ʈ" + playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].bit);
        //    Debug.Log("����" + playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].line);
        //    foreach (int j in playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].nowbit)
        //    {
        //        Debug.Log("��Ʈ ��ġ" + j);
        //    }
        //    if (playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].type != 0 && playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].type != 1)
        //    {
        //        foreach (int j in playsong.bars[i].notes[playsong.bars[i].notes.Count - 1].power)
        //        {
        //            Debug.Log("�̺�Ʈ ��" + j);
        //        }
        //    }
        //    Debug.Log("");
        playsong.measure = 4 * (60 / playsong.bpm);
        TickMs = playsong.measure;
        Instantiate(bar, new Vector3(gear_wid, playsong.measure * 1 * speed, 0.25f), new Quaternion());
        for (int i = 1; i <= 3; i++)
        {
            if (playsong.bars[i - 1].notes.Any<Line>())
            {
                foreach (Line l in playsong.bars[i - 1].notes)
                {
                    //Debug.Log("?" + playsong.bars[i - 1].notes.Count);gear_wid
                    //Debug.Log("��" + i);
                    //Debug.Log("Ÿ��" + l.type);
                    //Debug.Log("��Ʈ" + l.bit);
                    //Debug.Log("����" + l.line);
                    //foreach (int j in l.nowbit)
                    //{
                    //    Debug.Log("��Ʈ ��ġ" + j);
                    //}
                    //if (l.type != 0 && l.type != 1)
                    //{
                    //    foreach (int j in l.power)
                    //    {
                    //        Debug.Log("�̺�Ʈ ��" + j);
                    //    }
                    //}
                    //Debug.Log("");

                    int line = 20;
                    switch (l.line)
                    {
                        case 1: line = -4; break;
                        case 2: line = -2; break;
                        case 3: line = 0; break;
                        case 4: line = 2; break;
                        case 5: line = 4; break;
                    }
                    foreach (int a in l.nowbit)
                    {
                        if (l.type == 1 || l.type == 0)
                        {
                            Sprite skin;
                            if (l.type == 1)
                            {
                                if (l.line == 2 || l.line == 4) skin = Resources.Load<Sprite>($"skin/longnote{data.nownote}-1");
                                else skin = Resources.Load<Sprite>($"skin/longnote{data.nownote}-2");
                            }
                            else
                            {
                                if (l.line == 2 || l.line == 4) skin = Resources.Load<Sprite>($"skin/note{data.nownote}-1");
                                else skin = Resources.Load<Sprite>($"skin/note{data.nownote}-2");
                            }
                            GameObject OObject;
                            if (l.type == 1)
                            {
                                OObject = Instantiate(longnote, new Vector3(line + gear_wid, (playsong.measure * i * speed) + ((playsong.measure * speed) / l.bit) * a, 0.2f), new Quaternion());//GameObject.Find($"measure{i}").transform.position.y
                                if (long_wait[l.line - 1])
                                {
                                    longobj[l.line - 1].GetComponent<Long>().end = OObject;
                                    long_wait[l.line - 1] = false;
                                    OObject.GetComponent<Long>().type = false;
                                }
                                else
                                {
                                    longobj[l.line - 1] = OObject;
                                    long_wait[l.line - 1] = true;
                                    OObject.GetComponent<Long>().type = true;
                                }
                                OObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = skin;
                            }
                            else
                            {
                                OObject = Instantiate(note, new Vector3(line + gear_wid, (playsong.measure *i* speed) + ((playsong.measure * speed) / l.bit) * a, 0.2f), new Quaternion());//GameObject.Find($"measure{i}").transform.position.y
                                OObject.GetComponent<SpriteRenderer>().sprite = skin;
                            }
                            //OObject.name = $"{playsong.measure * (i) + (playsong.measure / l.bit) * a - playsong.measure}";
                            OObject.tag = $"line{l.line}";
                        }
                    }
                }
            }
            Instantiate(bar, new Vector3(gear_wid, playsong.measure * (i + 1) * speed, 0.25f), new Quaternion());
        }
        load.GetComponent<fead>().enabled = true;
        GameObject fakee = Instantiate(bar, new Vector3(gear_wid, playsong.measure * (playsong.bars.Count + 1) * speed, 0.25f), new Quaternion());
        fakee.tag = "last";
        fakee.GetComponent<SpriteRenderer>().enabled = false;
    }
    IEnumerator printnote(int measurecount)
    {
        yield return new WaitForSeconds(playsong.measure);
        if(playsong.bars.Count >= measurecount) {
            if (playsong.bars[measurecount - 1].notes.Any<Line>())
            {
                foreach (Line l in playsong.bars[measurecount - 1].notes)
                {
                    //Debug.Log("?" + playsong.bars[i - 1].notes.Count);gear_wid
                    //Debug.Log("��" + i);
                    //Debug.Log("Ÿ��" + l.type);
                    //Debug.Log("��Ʈ" + l.bit);
                    //Debug.Log("����" + l.line);
                    //foreach (int j in l.nowbit)
                    //{
                    //    Debug.Log("��Ʈ ��ġ" + j);
                    //}
                    //if (l.type != 0 && l.type != 1)
                    //{
                    //    foreach (int j in l.power)
                    //    {
                    //        Debug.Log("�̺�Ʈ ��" + j);
                    //    }
                    //}
                    //Debug.Log("");

                    int line = 20;
                    switch (l.line)
                    {
                        case 1: line = -4; break;
                        case 2: line = -2; break;
                        case 3: line = 0; break;
                        case 4: line = 2; break;
                        case 5: line = 4; break;
                    }
                    foreach (int a in l.nowbit)
                    {
                        if (l.type == 1 || l.type == 0)
                        {
                            Sprite skin;
                            if (l.type == 1)
                            {
                                if (l.line == 2 || l.line == 4) skin = Resources.Load<Sprite>($"skin/longnote{data.nownote}-1");
                                else skin = Resources.Load<Sprite>($"skin/longnote{data.nownote}-2");
                            }
                            else
                            {
                                if (l.line == 2 || l.line == 4) skin = Resources.Load<Sprite>($"skin/note{data.nownote}-1");
                                else skin = Resources.Load<Sprite>($"skin/note{data.nownote}-2");
                            }
                            GameObject OObject;
                            if (l.type == 1)
                            {
                                OObject = Instantiate(longnote, new Vector3(line + gear_wid, (playsong.measure * 3 * speed) + ((playsong.measure * speed) / l.bit) * a, 0.2f), new Quaternion());//GameObject.Find($"measure{measurecount}").transform.position.y
                                if (long_wait[l.line - 1])
                                {
                                    longobj[l.line - 1].GetComponent<Long>().end = OObject;
                                    long_wait[l.line - 1] = false;
                                    OObject.GetComponent<Long>().type = false;
                                }
                                else
                                {
                                    longobj[l.line - 1] = OObject;
                                    long_wait[l.line - 1] = true;
                                    OObject.GetComponent<Long>().type = true;
                                }
                                OObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = skin;
                            }
                            else
                            {
                                OObject = Instantiate(note, new Vector3(line + gear_wid, (playsong.measure * 3 * speed) + ((playsong.measure * speed) / l.bit) * a, 0.2f), new Quaternion());//GameObject.Find($"measure{measurecount}").transform.position.y
                                OObject.GetComponent<SpriteRenderer>().sprite = skin;
                            }
                            //OObject.name = $"{playsong.measure * (measurecount) + (playsong.measure / l.bit) * a - playsong.measure}";
                            OObject.tag = $"line{l.line}";
                        }
                    }
                }
            }
        }
        Instantiate(bar, new Vector3(gear_wid, playsong.measure * 4 * speed, 0.25f), new Quaternion());//GameObject.Find($"measure{measurecount}").transform.position.y +playsong.measure  * speed
        measurecount++;
        yield return StartCoroutine(printnote(measurecount));
    }
    public IEnumerator musingstart()
    {
        yield return new WaitForSeconds(2.1f);
        start = true;
        StartCoroutine(printnote(4));
        yield return new WaitForSeconds(playsong.measure);
        audio.Play();
        audio.volume = 1;
        gameObject.GetComponent<gameend>().enabled = true;
    }
}