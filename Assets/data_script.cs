using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class song
{
    public string name;
    public string writer;
    public string[] diffi;
    public int preview;
    public songdata[] songdata;
    public float bpm;
}
public class songdata
{
    public int score;
    public float acc;
    public string Tear;
    public int spe;
}
public enum keyAction { One, Two, Three, Three_s, Four, Five, Keycount }
public enum State { Select, Play, end }


public static class KeySetting { public static Dictionary<keyAction, KeyCode> keys = new Dictionary<keyAction, KeyCode>(); }
public enum gear_po { left, mid, right }
public class data_script : MonoBehaviour
{
    public AudioMixer MasterMixer;
    public State state;
    public int combo;
    public float fullcombo;
    public float score;
    public float speed;
    public List<song> song_list;
    public string nowsong_name;
    public string nowdiffi_name;
    public string artist;
    public int nowsong;
    public int nowdiffi;
    public int[] judge;
    public float sound;
    readonly public int gearcount=2;
    readonly public int notecount=2;
    public int nowgear;
    public int nownote;
    KeyCode[] defaultKeys = new KeyCode[] { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.K, KeyCode.L, KeyCode.Semicolon };
    public gear_po po = new gear_po();
    void Awake()
    {
        state = State.Select;
        nowdiffi = 0;
        nowsong = 0;
        judge = new int[6];
        init();
        po = gear_po.mid;
        speed = 28f;//1 = 3  2 = 8 3 = 13 4 = 18 5 = 23 6 = 28 7 = 33 8 = 38 9 = 43 10 = 48
        //Application.targetFrameRate = 60;
        StartCoroutine(startie());
    }
    public void init()
    {
        combo = 0;
        fullcombo = 0;
        score = 0;
        for (int i = 0; i < judge.Length; i++)
        {
            judge[i] = 0;
        }
    }
    public void gearchange(int p)
    {
        switch (p)
        {
            case 0:po = gear_po.mid;break;
            case 1: po = gear_po.right; break;
            case 2: po = gear_po.left; break;
        }
        StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
    }
    IEnumerator startie()
    {
        yield return new WaitForSeconds(0.01f);
        string fileName = "/Setting.csv";
        string filepath = Application.dataPath + fileName;
        string files = File.ReadAllText(filepath);
        using (StringReader sr = new StringReader(files))
        {
            string source = sr.ReadLine();
            string[] lines = source.Split(',');
            for (int i = 0; i < (int)keyAction.Keycount; i++)
            {
                defaultKeys[i] = (KeyCode)Int32.Parse(lines[i]);
                KeySetting.keys.TryAdd((keyAction)i, defaultKeys[i]);
            }
            source = sr.ReadLine();
            sound = float.Parse(source);
            if (sound == -40f) MasterMixer.SetFloat("Master", -80f);
            else MasterMixer.SetFloat("Master", sound);
            source = sr.ReadLine();
            switch (int.Parse(source))
            {
                case 0: po = gear_po.mid; break;
                case 1: po = gear_po.right; break;
                case 2: po = gear_po.left; break;
            }
            source = sr.ReadLine();
            nowsong = int.Parse(source);
            source = sr.ReadLine();
            speed = float.Parse(source);
            source = sr.ReadLine();
            nowgear = int.Parse(source.Split(',')[0]);
            nownote = int.Parse(source.Split(',')[1]);
        }
    }
    void Start()
    {
        song_list = new List<song>();
        if (GameObject.FindWithTag("save") != gameObject)
        {
            Destroy(gameObject);
        }
        else DontDestroyOnLoad(gameObject);

        
        TextAsset sourcefile = Resources.Load<TextAsset>("song_info");
        using (StringReader sr = new StringReader(sourcefile.text))
        {
            string source = sr.ReadLine();
            while (source != null)
            {
                string[] lines = source.Split(';');
                song Empty;
                Empty = new song();
                Empty.name = lines[0];
                Empty.writer = lines[1];
                Empty.diffi = lines[2].Split(',');
                Empty.preview = int.Parse(lines[3]);
                Empty.songdata = new songdata[4];
                Empty.bpm = float.Parse(lines[4]);
                song_list.Add(Empty);
                source = sr.ReadLine();
            }
        }
        string fileName = "/gamedata.csv";
        string filepath = Application.dataPath + fileName;
        string files = File.ReadAllText(filepath);
        using (StringReader sr = new StringReader(files))
        {
            int num = 0;
            string source = sr.ReadLine();
            while (source != null)
            {
                string[] data = source.Split(':');
                for (int i = 0; i < 4; i++)
                {
                    if (data[i] != null)
                    {
                        string[] sou = data[i].Split(';');//1000000;100;SS;2
                        song_list[num].songdata[i] = new songdata();
                        if (sou[0] == " ")
                        {
                            song_list[num].songdata[i].score = 0;
                            song_list[num].songdata[i].acc = 0;
                            song_list[num].songdata[i].Tear = "";
                            song_list[num].songdata[i].spe = 0;
                        }
                        else
                        {
                            song_list[num].songdata[i].score = int.Parse(sou[0]);
                            song_list[num].songdata[i].acc = float.Parse(sou[1]);
                            song_list[num].songdata[i].Tear = sou[2];
                            song_list[num].songdata[i].spe = int.Parse(sou[3]);
                        }
                    }
                }
                source = sr.ReadLine();
                num++;
            }
        }
    }
}