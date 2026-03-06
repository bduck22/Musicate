//using NUnit.Framework.Internal;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class select : MonoBehaviour
{
    public GameObject skinchanger;
    public GameObject[] move_ani;
    public GameObject esc_panel;
    public AudioMixer MasterMixer;
    public GameObject[] song_obj;
    public TMP_Text Song_name;
    public TMP_Text Song_Maker_Name;
    public Image thImage;
    public TMP_Text score;
    public TMP_Text tear;
    public TMP_Text acc;
    public TMP_Text spe;
    public TMP_Text bpm;
    public GameObject playlog;
    public GameObject load;
    public GameObject[] cursor;
    AudioSource au;
    Image[] song_image = new Image[11];
    data_script data;
    Image nowimage;
    public int nowsong;
    public diffcu_look diffi_cur;
    int fake_song;
    float time;
    float stime;
    float speedtime;
    float speedstime;
    void Start()
    {
        Time.timeScale = 1;
        time =0;
        stime=0;
        speedtime=0;
        speedstime=0;
        au = GetComponent<AudioSource>();
        nowimage = GetComponent<Image>();
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        data.state = State.Select;
        for (int i = 0; i < song_obj.Length; i++)
        {
            song_image[i] = song_obj[i].GetComponent<Image>();
        }
        fake_song = (nowsong) % (data.song_list.Count);
        StartCoroutine(init());
    }
    IEnumerator init()
    {
        yield return new WaitForSeconds(0.01f);
        nowsong = data.nowsong;
        yield return new WaitForSeconds(0.01f);
        StartCoroutine(imagesetting());
        yield return new WaitForSeconds(0.05f);
        songplay();
    }
    IEnumerator imagesetting()
    {
        yield return new WaitForSeconds(0.01f);
        diffi_cur.init();
        nowimage.sprite = Resources.Load<Sprite>(data.song_list[(nowsong) % (data.song_list.Count)].name + "_SEL");
        for (int i = 0, j = 0 + (nowsong+ (data.song_list.Count-4)); i < 9; i++, j++)
        {
            song_image[i].sprite = Resources.Load<Sprite>(data.song_list[j % (data.song_list.Count)].name + "_SEL");//i+1
        }
        bpm.text = data.song_list[(nowsong) % (data.song_list.Count)].bpm.ToString() + " BPM";
    }
    public void redata()
    {
        if (data.song_list[(nowsong) % (data.song_list.Count)].songdata[diffi_cur.diff_int].score == 0)
        {
            playlog.SetActive(true);
            score.text = "";
            acc.text = "";
        }
        else
        {
            playlog.SetActive(false);
            score.text = data.song_list[(nowsong) % (data.song_list.Count)].songdata[diffi_cur.diff_int].score.ToString();
            acc.text = data.song_list[(nowsong) % (data.song_list.Count)].songdata[diffi_cur.diff_int].acc.ToString("0.00")+"%";
        }
        tear.text = data.song_list[(nowsong) % (data.song_list.Count)].songdata[diffi_cur.diff_int].Tear;

        switch (data.song_list[(nowsong) % (data.song_list.Count)].songdata[diffi_cur.diff_int].spe)
        {
            case 0: spe.text = ""; break;
            case 1: spe.text = "AC"; break;
            case 2: spe.text = "AP"; break;
        }
    }
    void songplay()
    {
        Song_Maker_Name.text = data.song_list[(nowsong) % (data.song_list.Count )].writer;
        Song_name.text = data.song_list[(nowsong) % (data.song_list.Count)].name;
        thImage.sprite = Resources.Load<Sprite>(data.song_list[(nowsong) % (data.song_list.Count )].name + "_IMAGE");
        au.resource = Resources.Load<AudioResource>(data.song_list[(nowsong) % (data.song_list.Count )].name + "_AU");
        au.Play();
        au.time = data.song_list[(nowsong) % (data.song_list.Count)].preview;
        au.volume = 1;
        redata();
    }
    void playstart()
    {
        data.state = State.Play;
        switch (diffi_cur.diff_int)
        {
            case 0: data.nowdiffi_name = "star"; break;
            case 1: data.nowdiffi_name = "neb"; break;
            case 2: data.nowdiffi_name = "clu"; break;
            case 3: data.nowdiffi_name = "mag"; break;
        }
        data.nowdiffi = diffi_cur.diff_int;
        data.nowsong_name = data.song_list[(nowsong) % (data.song_list.Count)].name;
        data.nowsong = nowsong;
        data.artist = data.song_list[(nowsong) % (data.song_list.Count)].writer;
        load.GetComponent<Image>().sprite = Resources.Load<Sprite>(data.song_list[(nowsong) % (data.song_list.Count)].name + "_IMAGE");
        load.SetActive(true);
    }
    IEnumerator moveani(GameObject ob)
    {
        ob.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        yield return new WaitForSeconds(0.055f);
        ob.transform.localScale = new Vector3(2, 2, 2);
    }
    void Update()
    {
        if (!Cursor.visible) Cursor.visible = true;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!skinchanger.activeSelf)
            {
                skinchanger.SetActive(true);
                StartCoroutine(skinchanger.GetComponent<gear_noteskinchange>().imageload());
                skinchanger.GetComponent<gear_noteskinchange>().down = false;
            }
            else skinchanger.GetComponent<gear_noteskinchange>().down = true;
        }
        if (!skinchanger.activeSelf)
        {
            if(!diffi_cur.enabled)diffi_cur.enabled = true;
            if (!esc_panel.activeSelf && data.state == State.Select)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    speedtime = 0;
                    if (data.speed > 3) data.speed -= 0.5f;
                    else data.speed = 48;
                    StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
                }
                else if (Input.GetKey(KeyCode.Alpha1) && !Input.GetKey(KeyCode.Alpha2))
                {
                    if (speedtime > 0.28f)
                    {
                        speedstime += Time.deltaTime;
                        if (speedstime > 0.12f)
                        {
                            speedstime = 0;
                            if (data.speed > 3) data.speed -= 0.5f;
                            else data.speed = 48;
                            StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
                        }
                    }
                    else speedtime += Time.deltaTime;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha1))
                {
                    speedtime = 0;
                    speedstime = 0;
                }
                if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    speedtime = 0;
                    if (data.speed < 48) data.speed += 0.5f;
                    else data.speed = 3;
                    StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
                }
                else if (Input.GetKey(KeyCode.Alpha2) && !Input.GetKey(KeyCode.Alpha1))
                {
                    if (speedtime > 0.28f)
                    {
                        speedstime += Time.deltaTime;
                        if (speedstime > 0.12f)
                        {
                            speedstime = 0;
                            if (data.speed < 48) data.speed += 0.5f;
                            else data.speed = 3;
                            StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
                        }
                    }
                    else speedtime += Time.deltaTime;
                }
                else if (Input.GetKeyUp(KeyCode.Alpha2))
                {
                    speedtime = 0;
                    speedstime = 0;
                }

                if (Input.GetKeyDown(KeyCode.KeypadEnter))
                {
                    playstart();
                }
                if (Input.GetKeyDown(KeyCode.Return))
                {
                    playstart();
                }

                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    StartCoroutine(moveani(move_ani[0]));
                    fake_song = (nowsong) % (data.song_list.Count);
                    time = 0;
                    if (--nowsong < 0) nowsong = data.song_list.Count - 1;
                    StartCoroutine(imagesetting());
                }
                else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
                {
                    if (time > 0.4f)
                    {
                        stime += Time.deltaTime;
                        if (stime > 0.1f)
                        {
                            StartCoroutine(moveani(move_ani[0]));
                            stime = 0;
                            if (--nowsong < 0) nowsong = data.song_list.Count - 1;
                            diffi_cur.gameObject.SetActive(false);
                            StartCoroutine(imagesetting());
                        }
                    }
                    else time += Time.deltaTime;
                }
                else if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    diffi_cur.gameObject.SetActive(true);
                    if (fake_song != (nowsong) % (data.song_list.Count))
                    {
                        fake_song = (nowsong) % (data.song_list.Count);
                        songplay();
                    }
                    time = 0;
                    stime = 0;
                }

                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    StartCoroutine(moveani(move_ani[1]));
                    fake_song = (nowsong) % (data.song_list.Count);
                    time = 0;
                    if (++nowsong > (data.song_list.Count - 1)) nowsong = 0;
                    StartCoroutine(imagesetting());
                }
                else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
                {

                    if (time > 0.4f)
                    {
                        stime += Time.deltaTime;
                        if (stime > 0.1f)
                        {
                            StartCoroutine(moveani(move_ani[1]));
                            stime = 0;
                            if (++nowsong > (data.song_list.Count - 1)) nowsong = 0;
                            diffi_cur.gameObject.SetActive(false);
                            StartCoroutine(imagesetting());
                        }
                    }
                    else time += Time.deltaTime;
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    diffi_cur.gameObject.SetActive(true);
                    if (fake_song != (nowsong) % (data.song_list.Count))
                    {
                        fake_song = (nowsong) % (data.song_list.Count);
                        songplay();
                    }
                    time = 0;
                    stime = 0;
                }
            }
            if (au.time > data.song_list[fake_song].preview + 20)
            {
                au.volume -= 0.2f * Time.deltaTime;
            }
            if (au.volume <= 0)
            {
                au.volume = 1;
                au.Play();
                au.time = data.song_list[fake_song].preview;
            }
        }
        else if(diffi_cur.enabled)diffi_cur.enabled = false;
    }
}