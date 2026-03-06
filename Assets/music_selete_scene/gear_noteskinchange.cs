using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class gear_noteskinchange : MonoBehaviour
{
    GameObject gear_skin;
    Image[] gskins;
    GameObject note_skin;
    Image[] nskins;
    data_script data;
    float time2;
    float stime2;
    Vector2 target;
    Vector2 target2;
    public bool down;
    void Start()
    {
        down = false;
        target = Camera.main.WorldToScreenPoint(new Vector2(0, -3.72f));
        target2 = Camera.main.WorldToScreenPoint(new Vector2(0, -7.5f));
        time2 = 0;
        stime2 = 0;
        gear_skin = transform.GetChild(0).gameObject;
        note_skin = transform.GetChild(1).gameObject;
        gskins = new Image[3];
        nskins = new Image[3];
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        for(int i = 0; i < 3; i++)
        {
            gskins[i] = gear_skin.transform.GetChild(i + 1).GetComponent<Image>();
            nskins[i] = note_skin.transform.GetChild(i + 1).GetComponent<Image>();
        }
    }
    public IEnumerator imageload()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(GameObject.FindFirstObjectByType<save_test>().save_key_setting());
        for (int i = 0; i < 3; i++)
        {
            if (data.nowgear+i -1< 1 || data.nowgear + i -1> data.gearcount)
            {
                gskins[i].sprite = Resources.Load<Sprite>($"skin/black");
            }
            else
            {
                gskins[i].sprite = Resources.Load<Sprite>($"skin/gear{data.nowgear + i-1}");
            }
            if (data.nownote + i -1< 1||data.nownote+i-1>data.notecount)
            {
                nskins[i].sprite = Resources.Load<Sprite>($"skin/black");
            }
            else
            {
                nskins[i].sprite = Resources.Load<Sprite>($"skin/note{data.nownote + i-1}-2");
            }
        }
    }
    float time;
    float stime;
    void Update()
    {
        if(transform.position.y < target.y&&!down)
        {
            transform.position = Vector2.MoveTowards(transform.position, target, 1500 * Time.deltaTime);
        }
        else if(transform.position.y > target2.y&&down)
        {
            transform.position = Vector2.MoveTowards(transform.position, target2, 1500 * Time.deltaTime);
            if (transform.position.y == target2.y) gameObject.SetActive(false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (++data.nowgear > data.gearcount)
                {
                    data.nowgear = 1;
                }
                StartCoroutine(imageload());
            }
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                if (time > 0.4f)
                {
                    stime += Time.deltaTime;
                    if (stime > 0.09f)
                    {
                        stime = 0;
                        if (++data.nowgear > data.gearcount)
                        {
                            data.nowgear = 1;
                        }
                        StartCoroutine(imageload());
                    }
                }
                else time += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                time = 0;
                stime = 0;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (--data.nowgear < 1)
                {
                    data.nowgear = data.gearcount;
                }
                StartCoroutine(imageload());
            }
            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
            {
                if (time > 0.4f)
                {
                    stime += Time.deltaTime;
                    if (stime > 0.09f)
                    {
                        stime = 0;
                        if (--data.nowgear < 1)
                        {
                            data.nowgear = data.gearcount;
                        }
                        StartCoroutine(imageload());
                    }
                }
                else time += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                time = 0;
                stime = 0;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                time2 = 0;
                if (++data.nownote > data.notecount)
                {
                    data.nownote = 1;
                }
                StartCoroutine(imageload());
            }
            else if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                if (time2 > 0.4f)
                {
                    stime2 += Time.deltaTime;
                    if (stime2 > 0.09f)
                    {
                        stime2 = 0;
                        if (++data.nownote > data.notecount)
                        {
                            data.nownote = 1;
                        }
                        StartCoroutine(imageload());
                    }
                }
                else time2 += Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                time2 = 0;
                stime2 = 0;
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                time2 = 0;
                if (--data.nownote < 1)
                {
                    data.nownote = data.notecount  ;
                }
                StartCoroutine(imageload());
            }
            else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
            {

                if (time2 > 0.4f)
                {
                    stime2 += Time.deltaTime;
                    if (stime2 > 0.09f)
                    {
                        stime2 = 0;
                        if (--data.nownote < 1)
                        {
                            data.nownote = data.notecount ;
                        }
                        StartCoroutine(imageload());
                    }
                }
                else time2 += Time.deltaTime;
            }
            else if (Input.GetKeyUp(KeyCode.DownArrow))
            {
                time2 = 0;
                stime2 = 0;
            }
        }
    }
}