using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Text;
using UnityEngine.UI;
using UnityEngine.U2D;

public class result_script : MonoBehaviour
{
    data_script data;
    public Image bg;
    public TMP_Text Title;
    public TMP_Text Artist;
    public TMP_Text Score;
    public TMP_Text Tear;
    public TMP_Text spesial;
    public TMP_Text[] judge;
    public TMP_Text acc;
    public GameObject restart;
    public GameObject next;
    string[] judgestring;
    void Start()
    {
        int spenum = 0;
        judgestring = new string[6];
        judgestring[0] = "PERFECT";
        judgestring[1] = "GREAT";
        judgestring[2] = "CLEAR";
        judgestring[3] = "BAD";
        judgestring[4] = "HIT";
        judgestring[5] = "MISS";
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        bg.sprite = Resources.Load<Sprite>($"{data.nowsong_name}_IMAGE");
        Title.text = data.nowsong_name;
        Artist.text = data.artist;
        Score.text = "SCORE : " + Mathf.Round(data.score).ToString();
        float accnum = Mathf.Floor(data.score / 10000 * 100) / 100;
        acc.text = accnum.ToString("0.00")+"%";
        if(data.combo == data.fullcombo)
        {
            if (data.score == 1000000)
            {
                spenum = 2;
                spesial.text = "ALL PERFECT";
            }
            else
            {
                spenum = 1;
                spesial.text = "ALL CLEAR";
            }
        }
        switch (accnum)
        {
            case float n when n >= 95 && n <= 96:
                    Tear.text = "S-";
                break;
                case float n when n > 96 && n <= 98:
                    Tear.text = "S";
                break;
            case float n when n > 98 && n <= 99:
                Tear.text = "S+";
                break;
            case float n when n > 99 && n <= 100:
                Tear.text = "SS";
                break;

            case float n when n >= 90 && n <= 91.5f:
                Tear.text = "A-";
                break;
            case float n when n > 91.5f && n <= 93.5f:
                Tear.text = "A";
                break;
            case float n when n > 93.5f && n < 95:
                Tear.text = "A+";
                break;

            case float n when n >= 80 && n < 83.33f:
                Tear.text = "B-";
                break;
            case float n when n >= 83.33f && n <= 86.66f:
                Tear.text = "B";
                break;
            case float n when n > 86.66f && n < 90:
                Tear.text = "B+";
                break;

            case float n when n >= 70 && n < 73.33f:
                Tear.text = "C-";
                break;
            case float n when n >= 73.33f && n <= 76.66f:
                Tear.text = "C";
                break;
            case float n when n > 76.66f && n < 80:
                Tear.text = "C+";
                break;

            case float n when n >= 60 && n < 63.33f:
                Tear.text = "D-";
                break;
            case float n when n >= 63.33f && n <= 66.66f:
                Tear.text = "D";
                break;
            case float n when n > 66.66f && n < 70:
                Tear.text = "D+";
                break;

            default:
                Tear.text = "F";
            break;
        }
        for (int i = 0; i < judge.Length; i++)
        {
            judge[i].text = judgestring[i] + "\n"+data.judge[i];
        }
        string fileName = "/gamedata.csv";
        string path = Application.dataPath;
        string filepath = path + fileName;

        using (StreamWriter outStream = new StreamWriter(filepath))
        {
            for (int i = 0; i < data.song_list.Count; i++)
            {
                StringBuilder data = new StringBuilder("");
                for (int j = 0; j < 4; j++)
                {
                    if (i == (this.data.nowsong) % (this.data.song_list.Count) && j == this.data.nowdiffi)
                    {
                        if (this.data.song_list[i].songdata[j].score < Mathf.Round(this.data.score))
                        {
                            this.data.song_list[(this.data.nowsong) % (this.data.song_list.Count)].songdata[this.data.nowdiffi].score = (int)Mathf.Round(this.data.score);
                            this.data.song_list[(this.data.nowsong) % (this.data.song_list.Count)].songdata[this.data.nowdiffi].acc = accnum;
                            this.data.song_list[(this.data.nowsong) % (this.data.song_list.Count)].songdata[this.data.nowdiffi].Tear = Tear.text;
                            data.Append(Mathf.Round(this.data.score));
                            data.Append(";");
                            data.Append(accnum);
                            data.Append(";");
                            data.Append(Tear.text);
                            data.Append(";");
                        }
                        else
                        {
                            data.Append(this.data.song_list[i].songdata[j].score);
                            data.Append(";");
                            data.Append(this.data.song_list[i].songdata[j].acc);
                            data.Append(";");
                            data.Append(this.data.song_list[i].songdata[j].Tear);
                            data.Append(";");
                        }
                        if (this.data.song_list[i].songdata[j].spe < spenum)
                        {
                            this.data.song_list[(this.data.nowsong) % (this.data.song_list.Count)].songdata[this.data.nowdiffi].spe = spenum;
                            data.Append(spenum);
                        }
                        else data.Append(this.data.song_list[i].songdata[j].spe);
                        data.Append(":");
                    }
                    else if (this.data.song_list[i].songdata[j].score == 0)
                    {
                        data.Append(" ; ; ; :");
                    }
                    else
                    {
                        data.Append(this.data.song_list[i].songdata[j].score);
                        data.Append(";");
                        data.Append(this.data.song_list[i].songdata[j].acc);
                        data.Append(";");
                        data.Append(this.data.song_list[i].songdata[j].Tear);
                        data.Append(";");
                        data.Append(this.data.song_list[i].songdata[j].spe);
                        data.Append(":");
                    }
                }
                outStream.WriteLine(data);
            }
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            data.state = State.Select;
            next.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            data.state = State.Play;
            restart.GetComponent<Image>().sprite = Resources.Load<Sprite>($"{data.nowsong_name}_IMAGE");
            restart.SetActive(true);
        }
    }
}
