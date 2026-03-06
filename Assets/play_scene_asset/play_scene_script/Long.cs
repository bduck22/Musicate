using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Long : MonoBehaviour
{
    data_script data;
    public GameObject end;
    public bool type;
    float speed;
    public bool cantscan;
    public TMP_Text text;
    bool yesend;
    void Start()
    {
        yesend = false;
        data = GameObject.FindWithTag("save").GetComponent<data_script>();
        text = GameObject.FindWithTag("canvas").transform.GetChild(0).GetComponent<TMP_Text>();
        speed = FindAnyObjectByType<bms_test>().speed;
    }
    void Update()
    {
        if (type)
        {
            if (end)
            {
                if(!yesend)yesend = true;
                if (end.transform.position.y < transform.position.y)
                {
                    Destroy(gameObject);
                }
                else
                {
                    transform.localScale = new Vector3(1, Vector2.Distance(transform.position, end.transform.position), 0.2f);
                }
                if (end.transform.position.y < 0 - speed * 0.1f)
                {
                    Destroy(end);
                    Destroy(gameObject);
                }
            }
            else
            {
                if(yesend) Destroy(gameObject);
                else
                {
                    transform.localScale = new Vector3(1, 16, 0.2f);
                }
            }
            if (transform.position.y < -speed * 0.1f)
            {
                transform.position = new Vector3(transform.position.x, -speed * 0.1f+0.0001f, 0.2f);
                gameObject.GetComponent<move1>().enabled = false;
                end.GetComponent<Long>().cantscan = true;
                data.combo = 0;
                data.judge[5]+=2;
                text.text = "MISS";
                text.color = new Color(255, 0, 0);
            }
        }
        else
        {
            if(transform.localScale != new Vector3(1, 0.27f, 0.2f)) transform.localScale = new Vector3(1, 0, 0.2f);
        }
    }
}